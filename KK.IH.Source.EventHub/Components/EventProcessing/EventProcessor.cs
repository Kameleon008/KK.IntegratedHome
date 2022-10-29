namespace KK.IH.Source.EventHub.Components.EventProcessing
{
    using Azure.Messaging.EventHubs;
    using Azure.Messaging.EventHubs.Processor;
    using Azure.Storage.Blobs;
    using KK.IH.Source.EventHub.Components.EventProcessing.Configuration;
    using KK.IH.Source.EventHub.Components.Logger;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Concurrent;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class EventProcessor : IEventProcessor
    {
        EventProcessorClient internalProcesorClient;

        BlobContainerClient storageClient;

        EventProcessorClientOptions processorOptions;

        ILogger<EventProcessor> logger;

        IEventProcessorConfiguration configuration;

        public EventProcessor(ILogger<EventProcessor> logger, IEventProcessorConfiguration configuration)
        {

            this.logger = logger;
            this.configuration = configuration;


            this.processorOptions = new EventProcessorClientOptions
            {
                LoadBalancingStrategy = LoadBalancingStrategy.Greedy
            };

            this.storageClient = new BlobContainerClient(
                this.configuration.StorageConnectionString,
                this.configuration.StorageContainerName);

            this.internalProcesorClient = new EventProcessorClient(
                this.storageClient,
                this.configuration.EventHubConsumerGroup,
                this.configuration.EventHubConnectionString,
                this.processorOptions);

            var partitionEventCount = new ConcurrentDictionary<string, int>();

            this.internalProcesorClient.ProcessEventAsync += ProcessEventHandler;
            this.internalProcesorClient.ProcessErrorAsync += ProcessErrorHandler;
        }

        public Task StartProcessingAsync(CancellationToken cancellationToken)
        {
            return this.internalProcesorClient.StartProcessingAsync(cancellationToken);
        }

        public void StopProcessingAsync(CancellationToken cancellationToken)
        {
            this.internalProcesorClient.StopProcessing(cancellationToken);
        }

        static async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            StaticLogger.Information("Received event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        static Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            Console.WriteLine($"Partition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
