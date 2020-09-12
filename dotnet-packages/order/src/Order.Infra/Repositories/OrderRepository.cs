using Microsoft.EntityFrameworkCore;
using Order.Domain.Interfaces.Repositories;
using Order.Infra.Data;
using Order.Infra.Repository.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Infra.Repository
{
    public class OrderRepository : Repository<Domain.Entities.Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Domain.Entities.Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                      .Where(o => o.UserName == userName)
                      .ToListAsync();

            return orderList;
        }        
    }
}
