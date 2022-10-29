namespace KK.IH.Source.EventHub.Components.EventProcessing
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IEventProcessor
    {
        Task StartProcessingAsync(CancellationToken cancellationToken);

        void StopProcessingAsync(CancellationToken cancellationToken);
    }
}