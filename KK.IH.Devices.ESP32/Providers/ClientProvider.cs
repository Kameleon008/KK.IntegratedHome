using KK.IH.Devices.ESP32.Components.Appsettings;
using KK.IH.Devices.ESP32.Utility.Debug;
using nanoFramework.Azure.Devices.Client;
using nanoFramework.Networking;
using System;
using System.Security.Cryptography.X509Certificates;

namespace KK.IH.Devices.ESP32.Providers
{
    static class ClientProvider
    {
        public static void ProvideIotHubConnection(IAppsettings appsettings, ref DeviceClient client)
        {
            if (WifiNetworkHelper.Status != NetworkHelperStatus.NetworkIsReady)
            {
                Logger.Info($"Wifi network is not ready, actual state: {WifiNetworkHelper.Status}");
                return;
            }

            var azureRootCACert = new X509Certificate(Resources.GetBytes(Resources.BinaryResources.AzureRoot));
            client = new DeviceClient(appsettings.IotHubAddress, appsettings.DeviceId, appsettings.DeviceSasKey, azureCert: azureRootCACert);
            try
            {
                client.Open();
                Logger.Info($"Connected to Azure Iot Hub on address: {appsettings.IotHubAddress}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Could not open connection to Azure IoTHub on address: {appsettings.IotHubAddress}");
                Logger.Error($"Exception message {ex.Message}");
            }
        }
    }
}
