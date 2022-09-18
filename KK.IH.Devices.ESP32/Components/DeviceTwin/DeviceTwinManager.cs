namespace KK.IH.Devices.ESP32.Components.DeviceTwin
{
    using KK.IH.Devices.ESP32.Components.DeviceTwin.Desired;
    using KK.IH.Devices.ESP32.Components.DeviceTwin.Reported;
    using KK.IH.Devices.ESP32.Utility.Debug;
    using nanoFramework.Azure.Devices.Client;
    using nanoFramework.Azure.Devices.Shared;
    using System;
    using System.Collections;
    using System.Threading;

    public class DeviceTwinManager: IDesiredPropertiesPublisher
    {
        public ResportedProperties ReportedProperties { get; set; } = new ResportedProperties();

        public DesiredProperties DesiredProperties { get; private set; } = new DesiredProperties();

        private DeviceClient deviceClient;

        private IList desiredPropertiesSubscribers = new ArrayList();

        public DeviceTwinManager(DeviceClient client)
        {
            this.SetDeviceClient(client);
            this.GetActualDesiredProperties();
            this.SetUpdatedDesiredPropertiesHandler();
        }

        public void AddDesiredPropertiesSubscriber(IDesiredPropertiesSubscriber subscriber)
        {
            if (desiredPropertiesSubscribers.Add(subscriber) == -1)
            {
                Logger.Error($"Adding {subscriber.GetType().Name} on DesiredProperties subscriber list failed.");
            }

            Logger.Info($"Added {subscriber.GetType().Name} on DesiredProperties subscriber list.");
        }

        public void NotifyDesiredPropertiesSubscriber()
        {
            Logger.Info($"Updateing Desired Properties...");
            foreach (var subscriber in desiredPropertiesSubscribers)
            {
                var s = (IDesiredPropertiesSubscriber)subscriber;
                s.UpdateDesiredProperties(this.DesiredProperties);
            }

            Logger.Info($"Desired Properties updated.");
        }

        public void UpdateReportedProperties()
        {
            this.deviceClient.UpdateReportedProperties(this.ReportedProperties.AsTwinCollection());
        }

        private void DesiredPropertiesUpdated(object sender, TwinUpdateEventArgs desiredPropertiesUpdatedEvent)
        {
            Logger.Info($"Received Desired Properties {desiredPropertiesUpdatedEvent.Twin.ToJson()}");
            this.DesiredProperties.UpdateFromTwinCollection(desiredPropertiesUpdatedEvent.Twin);
            this.NotifyDesiredPropertiesSubscriber();
        }

        private void SetDeviceClient(DeviceClient client)
        {
            _ = client ?? throw new ArgumentNullException(nameof(client));
            this.deviceClient = client;
        }

        private void GetActualDesiredProperties()
        {
            var twin = this.deviceClient.GetTwin();
            this.DesiredProperties.UpdateFromTwinCollection(twin.Properties.Desired);
        }

        private void SetUpdatedDesiredPropertiesHandler()
        {
            this.deviceClient.TwinUpdated += DesiredPropertiesUpdated;
        }
    }
}
