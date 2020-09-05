using System;
using System.Threading.Tasks;

namespace Common.Core.Bus
{
    public interface IBusClient
    {
        Task ReceiveAsync<TMessage>(string queueName, Func<TMessage, Task> funcAsync) where TMessage : class;
        Task ReceiveAsync<TMessage>(string queueName, string exchangeName, Func<TMessage, Task> funcAsync) where TMessage : class;
        Task ReceiveAsync<TMessage>(string queueName, string exchangeName, string topic, Func<TMessage, Task> funcAsync) where TMessage : class;
    }
}