using RabbitMQ.Client;

namespace Common.Core.Bus.RabbitMq
{
    public interface IBusConnection
    {
        IConnection GetConnection();
    }
}