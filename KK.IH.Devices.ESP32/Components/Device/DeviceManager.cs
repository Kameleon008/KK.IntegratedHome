namespace KK.IH.Devices.ESP32.Components.Device
{
    using KK.IH.Devices.ESP32.Components.Appsettings;
    using KK.IH.Devices.ESP32.Providers;
    using nanoFramework.Azure.Devices.Client;

    class DeviceManager
    {
        public DeviceClient Client { get; private set; }

        public DeviceManager(IAppsettings appsettings)
        {
            this.Client = ClientProvider.ProvideIotHubConnection(appsettings);
        }
    }
}
