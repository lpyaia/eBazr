using Common.Core.Bus;
using Common.Core.Bus.RabbitMq;
using Common.Core.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Order.Consumer.Consumers;
using Order.CrossCutting;

namespace Order.Consumer
{
    public class Startup : BaseStartup
    {
        protected override void RegisterService(IServiceCollection services, IConfiguration config)
        {
            BootStrapper.RegisterServices(services, config);
            services.AddScoped<BasketCheckoutConsumer>();

            services.Configure<RabbitSettings>(x => config.GetSection(nameof(RabbitSettings)));

            services.AddScoped<IBusClient, BusClient>(x =>
            {
                var settings = x.GetRequiredService<IOptionsMonitor<RabbitSettings>>().CurrentValue;
                return new BusClient(settings.Url);
            });
        }
    }
}
