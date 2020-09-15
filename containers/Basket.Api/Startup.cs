using Basket.CrossCutting;
using Basket.Domain.Interfaces.Context;
using Basket.Domain.Interfaces.Repositories;
using Basket.Infra.Data;
using Basket.Infra.Repositories;
using Common.Core.Bus;
using Common.Core.Bus.RabbitMq;
using Common.Core.Common;
using Common.Core.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace Basket.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            BootStrapper.RegisterServices(services, Configuration);
            #region Rabbit
            services.AddSingleton<IBusPublisher, BusPublisher>();
            services.AddSingleton<IBusConnection>(x => 
                new BusConnection(Configuration.GetConnectionString(ConnectionStringNames.Rabbit)));
            #endregion

            #region Redis Dependencies

            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            #endregion

            #region Project Dependencies

            services.AddTransient<IBasketContext, BasketContext>();
            services.AddTransient<IBasketRepository, BasketRepository>();
            #endregion

            #region Swagger Dependencies

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket API", Version = "v1" });
            });

            #endregion

            #region RabbitMQ Dependencies

            services.Configure<RabbitSettings>(Configuration.GetSection(nameof(RabbitSettings)));

            services.AddScoped<IBusClient, BusClient>(x =>
            {
                var settings = x.GetRequiredService<IOptionsMonitor<RabbitSettings>>().CurrentValue;
                return new BusClient(settings.Url);
            });
            #endregion     


            services.AddControllers();       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket API V1");
            });
        }
    }
}
