using Common.Core.Common;
using Common.Core.Helpers;
using Common.Core.Logging;
using Common.Core.Logging.Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Config = Common.Core.Config;

namespace Framework.Core.Common
{
    public static class ConsoleBootstrap
    {
        public static async Task<int> RunAsync<TStartup>(Func<IServiceProvider, Task> execute)
            where TStartup : class
        {
            var type = typeof(TStartup);
            var startup = Activator.CreateInstance<TStartup>();

            var ret = await RunAsync((s, c) => type.GetMethod(nameof(BaseStartup.ConfigureServices))?.Invoke(startup, new object[] { s, c }),
                execute);

            return ret;
        }

        private static async Task<int> RunAsync(Action<IServiceCollection, IConfiguration> configure, Func<IServiceProvider, Task> execute)
        {
            var config = Config.Configuration.GetConfiguration();
            LogHelper.Logger = new SerilogLogger();

            try
            {
                LogHelper.Info($"Starting Console...");

                var services = new ServiceCollection();
                Bootstrapper.RegisterServices(services, config);

                configure(services, config);

                var provider = services.BuildServiceProvider();

                await execute(provider);

                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Console terminated unexpectedly.", ex);

#if DEBUG
                Console.ReadLine();
#endif
                return 1;
            }
            finally
            {
                PromptHelper.Wait();

                LogHelper.Info($"Exiting Console...");
            }
        }
    }
}