using StackExchange.Redis;

namespace Basket.Domain.Interfaces.Context
{
    public interface IBasketContext
    {
        IDatabase Redis { get; }
    }
}