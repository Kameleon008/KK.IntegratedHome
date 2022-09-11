namespace KK.IH.Devices.ESP32.Components.DeviceTwin.Desired
{
    public interface IDesiredPropertiesPublisher
    {
        public void AddDesiredPropertiesSubscriber(IDesiredPropertiesSubscriber subscriber);

        public void NotifyDesiredPropertiesSubscriber();

    }
}
