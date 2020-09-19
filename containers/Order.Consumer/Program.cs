using Common.Core.Bus;
using Common.Core.Logging;
using Framework.Core.Common;
using Microsoft.Extensions.DependencyInjection;
using Order.Consumer.Consumers;
using Order.Core.Common;
using Order.Domain.Events;
using Order.Infra.Data;
using System;
using System.Threading.Tasks;

namespace Order.Consumer
{
    public class Program
    {
        protected Program()
        {
        }

        public static async Task<int> Main()
        {
            return await ConsoleBootstrap.RunAsync<Startup>(Execute);
        }

        private static async Task Execute(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var aspnetRunContext = services.GetRequiredService<OrderContext>();
                    OrderContextSeed.SeedAsync(aspnetRunContext).Wait();
                }
                catch (Exception exception)
                {
                    LogHelper.Error("An error occurred seeding the DB.", exception);
                }
            }

            var client = provider.GetRequiredService<IBusClient>();

            await client.ReceiveAsync<BasketCheckoutEvent>(ContextNames.Queue.OrderCheckout, ContextNames.Exchange.BasketCheckout,
                async message =>
                {
                    using var scope = provider.CreateScope();
                    var consumer = scope.ServiceProvider.GetRequiredService<BasketCheckoutConsumer>();
                    await consumer.Execute(message);
                });
        }
    }
}