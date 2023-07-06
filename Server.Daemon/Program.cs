using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Server.Daemon.Worker;

namespace Server.Daemon
{
    /// <summary>
    /// <c>Program</c> is the daemon's entrypoint. Running it launches the
    /// application's background workers.
    /// </summary>
    class Program
    {
        /// <summary>
        /// <c>Main</c> launches the application. Command-line arguments and
        /// environment variables are accepted but currently not used.
        /// </summary>
        public static async Task Main(string []args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();

                    if (args != null) config.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();

                    services.Configure<MirConfig>(hostContext.Configuration.GetSection("Daemon"));

                    // Spawn workers
                    services.AddSingleton<IHostedService, GameLoop>();
                    services.AddSingleton<IHostedService, GameLog>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await builder.RunConsoleAsync();
        }
    }
}
