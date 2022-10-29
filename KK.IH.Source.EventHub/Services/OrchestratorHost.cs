namespace KK.IH.Source.EventHub
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System.Threading;
    using System.Threading.Tasks;

    public class OrchestratorHost : IHostedService
    {
        ILogger<OrchestratorHost> logger;
        public OrchestratorHost(ILogger<OrchestratorHost> logger)
        {
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("IHostedService Action");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
