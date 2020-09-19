using System.Threading.Tasks;
using Web.App.Models;

namespace Web.App.ApiCollection.Interfaces
{
    public interface IBasketApi
    {
        Task<BasketModel> GetBasket(string userName);

        Task<BasketModel> UpdateBasket(BasketModel model);

        Task CheckoutBasket(BasketCheckoutModel model);
    }
}