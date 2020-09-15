using Common.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Infra.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                orderContext.Database.Migrate();

                if (!orderContext.Orders.Any())
                {
                    orderContext.Orders.AddRange(GetPreconfiguredOrders());
                    await orderContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 5)
                {
                    retryForAvailability++;
                    LogHelper.Error(exception.Message);
                    await SeedAsync(orderContext, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Domain.Entities.Order> GetPreconfiguredOrders()
        {
            return new List<Domain.Entities.Order>()
            {
                new Domain.Entities.Order() { UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "meh@ozk.com", AddressLine = "Bahcelievler", TotalPrice = 5239 },
                new Domain.Entities.Order() { UserName = "swn", FirstName = "Selim", LastName = "Arslan", EmailAddress ="sel@ars.com", AddressLine = "Ferah", TotalPrice = 3486 }
            };
        }
    }
}
