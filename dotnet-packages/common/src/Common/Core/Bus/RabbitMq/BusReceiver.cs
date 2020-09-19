using Common.Core.Config;
using Common.Core.Helpers;
using Common.Core.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.Bus.RabbitMq
{
    public class BusReceiver : BaseBus, IBusReceiver
    {
        public BusReceiver(IBusConnection busConnection)
            : base(busConnection)
        {
        }

        public Task ReceiveAsync(string queueName, string exchangeName, string topic, Func<MessageInfo, Task> funcAsync)
        {
            var channel = Connection.CreateModel();

            var args = CreateDeadLetterPolicy(channel);

            channel.QueueDeclare(queueName, true, false, false, args);
            channel.BasicQos(0, 10, false);

            if (exchangeName != null)
            {
                var isTopic = !string.IsNullOrEmpty(topic);

                channel.ExchangeDeclare(exchangeName, isTopic ? ExchangeType.Topic : ExchangeType.Fanout, true, false, null);
                channel.QueueBind(queueName, exchangeName, isTopic ? topic : queueName);
            }

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                await HandlingMessage(queueName, funcAsync, ea, channel);
            };

            channel.BasicConsume(queueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandlingMessage(string queueName, Func<MessageInfo, Task> funcAsync, BasicDeliverEventArgs ea, IModel channel)
        {
            var debugging = Configuration.Debugging.Get();
            var body = ea.Body;
            var messageInfo = new MessageInfo
            {
                ContentName = ea.BasicProperties.ContentType,
                MessageId = ea.BasicProperties.MessageId,
                RequestId = ea.BasicProperties.CorrelationId,
                Priority = ea.BasicProperties.Priority,
                Body = Encoding.UTF8.GetString(body.ToArray())
            };

            try
            {
                await funcAsync(messageInfo);

                channel.BasicAck(ea.DeliveryTag, false);

                if (debugging)
                    LogHelper.Debug($"Processed {queueName}: {messageInfo.MessageId}");
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Error {queueName}: {ex.Message}.", ex);

                var retryType = HandlingErrorMessage(ea, ex);

                switch (retryType)
                {
                    case ERetryType.Resend:
                        channel.BasicPublish(string.Empty, queueName, ea.BasicProperties, ea.Body);
                        channel.BasicAck(ea.DeliveryTag, false);
                        break;

                    case ERetryType.Reject:
                        channel.BasicReject(ea.DeliveryTag, false);
                        break;

                    default:
                        channel.BasicNack(ea.DeliveryTag, false, true);
                        break;
                }

                Task.Delay(500).Wait();
            }
        }

        private ERetryType HandlingErrorMessage(BasicDeliverEventArgs ea, Exception ex)
        {
            var propsMessage = ea.BasicProperties;

            //handling old version of rabbitMQ
            if (propsMessage.Headers == null)
                propsMessage.Headers = new Dictionary<string, object>();

            if (!ea.Redelivered && propsMessage.Headers != null && !propsMessage.Headers.ContainsKey("x-retry-count")) return ERetryType.Retry;
            if (ex != null && !propsMessage.Headers.ContainsKey("ExceptionMessage"))
            {
                propsMessage.Headers.Add("ExceptionMessage", ex.Message);
                propsMessage.Headers.Add("ExceptionStackTrace", ex.StackTrace);
                propsMessage.Headers.Add("ExceptionInnerException", ex.InnerException?.ToString());
                propsMessage.Headers.Add("Date", DateTime.UtcNow.ToString(DateHelper.DefaultFormat));
            }
            else
            {
                propsMessage.Headers["ExceptionMessage"] = ex?.Message;
                propsMessage.Headers["ExceptionStackTrace"] = ex?.StackTrace;
                propsMessage.Headers["ExceptionInnerException"] = ex?.InnerException?.ToString();
                propsMessage.Headers["Date"] = DateTime.UtcNow.ToString(DateHelper.DefaultFormat);
            }

            var requeue = HandlingRetryMessage(propsMessage);
            return requeue;
        }

        private static ERetryType HandlingRetryMessage(IBasicProperties propsMessage)
        {
            if (!propsMessage.Headers.ContainsKey("x-retry-count"))
                propsMessage.Headers.Add("x-retry-count", 0);

            if (!propsMessage.Headers.ContainsKey("x-total-retry-count"))
                propsMessage.Headers.Add("x-total-retry-count", 0);

            var retryCount = (int)propsMessage.Headers["x-retry-count"];
            if (retryCount <= 3)
            {
                retryCount++;
                propsMessage.Headers["x-retry-count"] = retryCount;
            }
            else
            {
                var priority = int.Parse(propsMessage.Priority.ToString()) + 1;
                propsMessage.Priority = byte.Parse(priority.ToString());
                propsMessage.Headers["x-retry-count"] = 0;
            }

            int totalRetryCount = (int)propsMessage.Headers["x-total-retry-count"];
            totalRetryCount += 1;
            propsMessage.Headers["x-total-retry-count"] = totalRetryCount;

            return propsMessage.Priority <= 3 ? ERetryType.Resend : ERetryType.Reject;
        }
    }
}