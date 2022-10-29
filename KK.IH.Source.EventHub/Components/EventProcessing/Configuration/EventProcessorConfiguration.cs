namespace KK.IH.Source.EventHub.Components.EventProcessing.Configuration
{
    public class EventProcessorConfiguration : IEventProcessorConfiguration
    {
        public string StorageConnectionString { get; set; }
        public string StorageContainerName { get; set; }
        public string EventHubConnectionString { get; set; }
        public string EventHubConsumerGroup { get; set; }
    }
}
