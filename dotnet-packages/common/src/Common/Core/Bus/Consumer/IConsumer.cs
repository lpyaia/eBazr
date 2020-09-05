using System.Threading.Tasks;

namespace Common.Core.Bus.Consumer
{
    public interface IConsumer<in TMessage>
         where TMessage : IBusMessage
    {
        Task Execute(TMessage message);
    }
}