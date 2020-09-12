using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Entities.Order>
    {
        Task<IEnumerable<Entities.Order>> GetOrdersByUserName(string userName);
    }
}
