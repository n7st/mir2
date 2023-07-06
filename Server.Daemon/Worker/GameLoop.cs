using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Server;
using Server.MirEnvir;
using Server.MirDatabase;

namespace Server.Daemon.Worker
{
    public class GameLoop : IHostedService, IDisposable
    {
        private readonly ILogger _logger;

        private readonly IOptions<MirConfig> _config;

        private readonly Envir _envir = Envir.Main;

        private readonly Envir _editEnvir = Envir.Edit;

        public GameLoop(ILogger<GameLoop> logger, IOptions<MirConfig> config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Configure();

            await Task.Run(async () =>
            {
                if (_editEnvir.LoadDB())
                {
                    _envir.Start();

                    _logger.LogInformation("Started the server");
                }
                else
                {
                    _logger.LogError("Failed to start server");
                }

                await Task.CompletedTask;
            });
        }

        public void OnStarted()
        {
            _logger.LogInformation("Starting Server.Daemon");
        }

        public void OnStopping()
        {
            _logger.LogInformation("Stopping Server.Daemon");
        }

        /// <summary>
        /// <c>StopAsync</c> cleans up unsaved data and then stops the server.
        /// </summary>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_envir.Running) _envir.Stop();

            _envir.MonsterCount = 0;

            Settings.Save();

            return Task.CompletedTask;
        }

        /// <summary>
        /// <c>Dispose</c> cleans up any unmanaged resources before the daemon exits.
        /// </summary>
        public void Dispose()
        {
            // TODO: cleanup
            _logger.LogInformation("Disposing...");
        }

        private void Configure()
        {
            Packet.IsServer = true;

            Settings.Load();

            // The below settings are overridden as temporary measures for
            // debugging purposes
            Settings.AllowStartGame = _config.Value.AllowStartGame;
            Settings.Port = _config.Value.Port;
            Settings.CheckVersion = _config.Value.CheckVersion;
            Settings.Multithreaded = _config.Value.Multithreaded;
        }
    }
}
