namespace KK.IH.Devices.ESP32.Components.DeviceTwin.Desired
{
    public interface IDesiredPropertiesSubscriber
    {
        public void UpdateDesiredProperties(DesiredProperties desiredProperties);
    }
}
