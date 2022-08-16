using KK.IH.Devices.ESP32.Components.Appsettings;
using nanoFramework.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KK.IH.Devices.ESP32.Providers
{
    static class ClientProvider
    {
        public static void ProvideIotHubConnection(IAppsettings appsettings, ref DeviceClient client)
        {
            client = new DeviceClient(appsettings.IotHubAddress, appsettings.DeviceId, appsettings.DeviceSasKey);

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
