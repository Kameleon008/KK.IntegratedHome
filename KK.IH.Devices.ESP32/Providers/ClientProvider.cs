using KK.IH.Devices.ESP32.Components.Appsettings;
using nanoFramework.Azure.Devices.Client;
using nanoFramework.Networking;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace KK.IH.Devices.ESP32.Providers
{
    static class ClientProvider
    {
        public static void ProvideIotHubConnection(IAppsettings appsettings, ref DeviceClient client)
        {
            if (WifiNetworkHelper.Status != NetworkHelperStatus.NetworkIsReady)
            {
                Debug.WriteLine($"Wifi network is not ready, actual state: {WifiNetworkHelper.Status}");
                return;
            }

            var azureRootCACert = new X509Certificate(Resources.GetBytes(Resources.BinaryResources.AzureRoot));
            client = new DeviceClient(appsettings.IotHubAddress, appsettings.DeviceId, appsettings.DeviceSasKey, azureCert: azureRootCACert);
            try
            {
                client.Open();
                Debug.WriteLine($"Connected to Azure Iot Hub on address: {appsettings.IotHubAddress}");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error while connecting to Azure Iot Hub on address: {appsettings.IotHubAddress}");
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
