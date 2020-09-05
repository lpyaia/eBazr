using System;
using System.Threading.Tasks;

namespace Common.Core.Bus
{
    public interface IBusReceiver
    {
        Task ReceiveAsync(string queueName, string exchangeName, string topic, Func<MessageInfo, Task> funcAsync);
    }
}