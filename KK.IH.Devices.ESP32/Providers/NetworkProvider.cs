namespace KK.IH.Devices.ESP32.Providers
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using nanoFramework.Hardware.Esp32;
    using nanoFramework.Networking;
    using KK.IH.Devices.ESP32.Components.Appsettings;

    static class NetworkProvider
    {
        public static void ProvideWifiConnection(IAppsettings appsettings)
        {
            CancellationTokenSource cs = new(5);
            var success = WifiNetworkHelper.ConnectDhcp(appsettings.WifiName, appsettings.WifiPassword, requiresDateTime: true, token: cs.Token);
            if (!success)
            {
                Debug.WriteLine($"Error  while trying to connect to {appsettings.WifiName}");
                Sleep.EnableWakeupByTimer(TimeSpan.FromMinutes(5));
                Sleep.StartDeepSleep();
            }

            Debug.WriteLine($"Connected To {appsettings.WifiName}");
        }
    }
}
