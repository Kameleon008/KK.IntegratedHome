namespace KK.IH.Source.EventHub.Components.EventProcessing.Configuration
{
    public interface IEventProcessorConfiguration
    {
        string StorageConnectionString { get; set; }

        string StorageContainerName { get; set; }

        string EventHubConnectionString { get; set; }

        string EventHubConsumerGroup { get; set; }
    }
}