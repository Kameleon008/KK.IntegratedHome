namespace KK.IH.Devices.ESP32.Components.Appsettings
{
    public interface IAppsettingsManager
    {
        public Appsettings GetAppsettings();

        public bool SaveAppsettings(Appsettings appsettings);
    }
}
