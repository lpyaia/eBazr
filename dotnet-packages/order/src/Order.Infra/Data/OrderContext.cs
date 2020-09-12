using Microsoft.EntityFrameworkCore;

namespace Order.Infra.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Domain.Entities.Order> Orders { get; set; }
    }
}
