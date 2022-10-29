namespace KK.IH.Source.EventHub
{
    using KK.IH.Source.EventHub.Components.EventProcessing;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System.Threading;
    using System.Threading.Tasks;

    public class OrchestratorHost : IHostedService
    {
        private readonly ILogger<OrchestratorHost> logger;
        private readonly IEventProcessor eventProcessor;

        public OrchestratorHost(ILogger<OrchestratorHost> logger, IEventProcessor eventProcessor)
        {
            this.logger = logger;
            this.eventProcessor=eventProcessor;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return this.eventProcessor.StartProcessingAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.eventProcessor.StopProcessingAsync(cancellationToken);
            return Task.CompletedTask;
        }
    }
}
