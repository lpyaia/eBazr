using Common.Core.Common;
using Common.Core.Config;
using Common.Core.Logging;
using Common.Core.Logging.Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Common.Core.Bus.Consumer
{
    public static class ConsumerBootstrap
    {
        public static async Task<int> RunAsync<TStartup>()
            where TStartup : class
        {
            var type = typeof(TStartup);
            var startup = Activator.CreateInstance<TStartup>();

            var ret = await RunAsync((s, c) => type.GetMethod(nameof(BaseStartup.ConfigureServices))?.Invoke(startup, new object[] { s, c }),
                (h) => type.GetMethod(nameof(BaseStartup.AddConsumers))?.Invoke(startup, new object[] { h }));

            return ret;
        }

        private static async Task<int> RunAsync(Action<IServiceCollection, IConfiguration> configure, Action<IConsumerHandler> addConsumers)
        {
            var config = Configuration.GetConfiguration();
            LogHelper.Logger = new SerilogLogger();

            try
            {
                LogHelper.Info("Starting Consumer...");

                var services = new ServiceCollection();

                Bootstrapper.RegisterServices(services, config, (s, c) =>
                {
                    s.AddScoped<IConsumerHandler, ConsumerHandler>();
                });

                configure(services, config);

                var provider = services.BuildServiceProvider();

                var manager = provider.GetRequiredService<IConsumerHandler>();
                addConsumers(manager);

                await manager.StartAsync();

                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Consumer terminated unexpectedly.", ex);

#if DEBUG
                Console.ReadLine();
#endif
                return 1;
            }
            finally
            {
                Task.Delay(500).Wait();
                LogHelper.Info("Exiting Consumer...");
            }
        }
    }
}