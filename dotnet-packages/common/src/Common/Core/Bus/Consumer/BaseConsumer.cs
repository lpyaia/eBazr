using System.Threading.Tasks;

namespace Common.Core.Bus.Consumer
{
    public abstract class BaseConsumer<TMessage> : IConsumer<TMessage>
       where TMessage : class, IBusMessage
    {
        public abstract Task Execute(TMessage message);
    }
}