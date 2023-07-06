using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Server.Daemon.Worker
{
    /// <summary>
    /// <c>GameLog</c> is a background service which watches the game server's
    /// log message queue and converts the output to log lines.
    /// </summary>
    public class GameLog : BackgroundService
    {
        private readonly ILogger _logger;

        private readonly MessageQueue _messageQueue = MessageQueue.Instance;

        public GameLog(ILogger<GameLog> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Server.Daemon.Utility.LogProcessor");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    foreach (var log in _messageQueue.Logs)
                    {
                        ProcessMessageLog(log);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }

                await Task.Delay(5000, cancellationToken);
            }
        }

        private void ProcessMessageLog(ConcurrentQueue<string> messageLog)
        {
            while (!messageLog.IsEmpty)
            {
                if (!messageLog.TryDequeue(out var message)) continue;

                _logger.LogInformation(message);
            }
        }
    }
}