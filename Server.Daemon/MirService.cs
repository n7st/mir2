using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Server.Daemon
{
    public class MirService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;

        private readonly IOptions<MirConfig> _config;

        public MirService(ILogger<MirService> logger, IOptions<MirConfig> config)
        {
            _logger = logger;
            _config = config;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Server.Daemon: " + _config.Value.DaemonName);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping Server.Daemon");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("Disposing...");
        }
    }
}
