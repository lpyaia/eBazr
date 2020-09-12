using Common.Core.Bus;
using Framework.Core.Common;
using Microsoft.Extensions.DependencyInjection;
using Order.Consumer.Consumers;
using Order.Core.Common;
using Order.Domain.Events;
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
            var client = provider.GetRequiredService<IBusClient>();

            await client.ReceiveAsync<BasketCheckoutEvent>(ContextNames.Queue.OrderCheckout, ContextNames.Exchange.BasketCheckout,
                message =>
                {
                    using var scope = provider.CreateScope();
                    var consumer = scope.ServiceProvider.GetRequiredService<BasketCheckoutConsumer>();
                    return consumer.Execute(message);
                });
        }
    }
}
