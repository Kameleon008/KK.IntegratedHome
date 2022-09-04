namespace KK.IH.Devices.ESP32.Providers
{
    using System.Threading;
    using nanoFramework.Networking;
    using KK.IH.Devices.ESP32.Components.Appsettings;
    using KK.IH.Devices.ESP32.Utility.Debug;

    static class NetworkProvider
    {
        private static int WifiConnectingTimeoutMs = 60000;

        public static void ProvideWifiConnection(IAppsettings appsettings)
        {
            CancellationTokenSource cs = new(WifiConnectingTimeoutMs);
            var success = WifiNetworkHelper.ConnectDhcp(appsettings.WifiName, appsettings.WifiPassword, requiresDateTime: true, token: cs.Token);
            if (!success)
            {
                Logger.Error($"Could not connect to: {appsettings.WifiName}");
                Logger.Error($"Wifi connection status: {nameof(WifiNetworkHelper.Status)}");
                return;
            }

            Logger.Info($"Connected To {appsettings.WifiName}");
            Logger.Info($"Connection status {WifiNetworkHelper.Status}");
        }
    }
}
