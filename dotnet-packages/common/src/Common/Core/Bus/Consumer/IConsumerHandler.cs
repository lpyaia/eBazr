using System.Threading.Tasks;

namespace Common.Core.Bus.Consumer
{
    public interface IConsumerHandler
    {
        Task StartAsync();

        void SetContainer(IConsumerContainer container);
    }
}