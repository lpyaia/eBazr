using Common.Core.Bus;
using Common.Core.Bus.RabbitMq;
using Common.Core.Common;
using Common.Core.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Consumer.Consumers;
using Order.CrossCutting;
using Order.Domain.Interfaces.Repositories;
using Order.Infra.Data;
using Order.Infra.Repository;
using Order.Infra.Repository.Base;

namespace Order.Consumer
{
    public class Startup : BaseStartup
    {
        protected override void RegisterService(IServiceCollection services, IConfiguration config)
        {
            BootStrapper.RegisterServices(services, config);

            #region SqlServer Dependencies

            services.AddDbContext<OrderContext>(c =>
                c.UseSqlServer(config.GetConnectionString(ConnectionStringNames.Sql)));

            #endregion

            #region Project Dependencies

            // Add Infrastructure Layer
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddTransient<IOrderRepository, OrderRepository>(); // we made transient this in order to resolve in mediatR when consuming Rabbit

            #endregion

            services.AddScoped<BasketCheckoutConsumer>();

            services.Configure<RabbitSettings>(x => config.GetSection(nameof(RabbitSettings)));

            services.AddSingleton<IBusClient, BusClient>(x =>
            {
                return new BusClient(config.GetConnectionString(ConnectionStringNames.Rabbit));
            });
        }
    }
}
