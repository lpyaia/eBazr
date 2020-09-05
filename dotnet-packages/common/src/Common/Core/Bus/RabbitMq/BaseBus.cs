using RabbitMQ.Client;
using System.Collections.Generic;

namespace Common.Core.Bus.RabbitMq
{
    public class BaseBus
    {
        private readonly IBusConnection _busConnection;

        public BaseBus(IBusConnection busConnection)
        {
            _busConnection = busConnection;
        }

        public Dictionary<string, object> CreateDeadLetterPolicy(IModel channel)
        {
            var dlxExhange = $"DLX";
            var dlxQueue = "Messages";
            channel.ExchangeDeclare(dlxExhange, ExchangeType.Fanout, true, false, null);
            channel.QueueDeclare(dlxQueue, true, false, false, null);
            channel.QueueBind(dlxQueue, dlxExhange, string.Empty, null);
            var args = new Dictionary<string, object> { { "x-dead-letter-exchange", dlxExhange }, { "x-max-priority", 10 } };
            return args;
        }

        protected IConnection Connection => _busConnection.GetConnection();
    }
}