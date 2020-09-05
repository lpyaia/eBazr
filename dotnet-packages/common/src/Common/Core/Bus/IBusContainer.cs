using System.Threading.Tasks;

namespace Common.Core.Bus
{
    public interface IBusContainer : IBusPublisher
    {
        Task Commit();
    }
}