namespace KK.IH.Devices.ESP32.Providers
{
    using System.Diagnostics;
    using System.Threading;
    using nanoFramework.Networking;
    using KK.IH.Devices.ESP32.Components.Appsettings;

    static class NetworkProvider
    {
        private static int WifiConnectingTimeoutMs = 60000;

        public static void ProvideWifiConnection(IAppsettings appsettings)
        {
            CancellationTokenSource cs = new(WifiConnectingTimeoutMs);
            var success = WifiNetworkHelper.ConnectDhcp(appsettings.WifiName, appsettings.WifiPassword, requiresDateTime: true, token: cs.Token);
            if (!success)
            {
                Debug.WriteLine($"Error while trying to connect to {appsettings.WifiName}");
                Debug.WriteLine($"Error status {nameof(WifiNetworkHelper.Status)}");
                return;
            }

            Debug.WriteLine($"Connected To {appsettings.WifiName}");
            Debug.WriteLine($"Connection status {WifiNetworkHelper.Status}");
        }
    }
}
