namespace KK.IH.Devices.ESP32.Components.DeviceTwin.Reported
{
    using nanoFramework.Azure.Devices.Shared;

    public class ResportedProperties
    {
        public TwinCollection AsTwinCollection()
        {
            return new TwinCollection();
        }
    }
}
