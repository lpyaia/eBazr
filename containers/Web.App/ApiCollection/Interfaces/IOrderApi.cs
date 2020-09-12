using Web.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.App.ApiCollection.Interfaces
{
    public interface IOrderApi
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
