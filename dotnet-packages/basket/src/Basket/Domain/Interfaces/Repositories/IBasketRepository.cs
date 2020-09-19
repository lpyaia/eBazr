using Basket.Domain.Entities;
using System.Threading.Tasks;

namespace Basket.Domain.Interfaces.Repositories
{
    public interface IBasketRepository
    {
        Task<BasketCart> GetBasket(string userName);

        Task<BasketCart> UpdateBasket(BasketCart basket);

        Task<bool> DeleteBasket(string userName);
    }
}