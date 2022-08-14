using System.Collections;
using Iot.Device.Bmxx80.FilteringMode;
using KK.IH.Devices.ESP32.Hardware.Sensors;
using KK.IH.Devices.ESP32.Hardware.Sensors.Bmp280;
using nanoFramework.Json;
using UnitsNet.Units;

namespace KK.IH.Devices.ESP32
{
    using Iot.Device.Bmxx80;
    using nanoFramework.Azure.Devices.Client;
    using nanoFramework.Hardware.Esp32;
    using nanoFramework.Networking;
    using System;
    using System.Device.I2c;
    using System.Diagnostics;
    using System.Threading;
    using Components.Appsettings;


    public class Program
    {
        public static void Main()
        {
            int sleepTimeMinutes = 60000;
            int secondsToGoToSleep = 5;
            int busIdI2C;

            IAppsettings appsettings;
            IAppsettingsManager appsettingsManager;

            SensorBmp280 sensorBmp280;
            DeviceClient azureIoT;

            InitializeAppsettings();
            InitializeInterfaceI2C();
            InitializeSensorBmp280();
            ConnectToWifi();
            ConnectIotHub();

            while (true)
            {
                IList readResult = new ArrayList();
                readResult.Add(sensorBmp280.GetMeasurements());

                string messageContent = JsonConvert.SerializeObject(readResult);
                azureIoT.SendMessage(messageContent, new CancellationTokenSource(2000).Token);
                Debug.WriteLine(messageContent);

                Thread.Sleep(secondsToGoToSleep * 1000);
            }

            void GoToSleep()
            {
                Sleep.EnableWakeupByTimer(TimeSpan.FromMinutes(secondsToGoToSleep));
                Sleep.StartDeepSleep();
            }

            void ConnectToWifi()
            {
                CancellationTokenSource cs = new(sleepTimeMinutes);
                var success = WifiNetworkHelper.ConnectDhcp(appsettings.WifiName, appsettings.WifiPassword, requiresDateTime: true, token: cs.Token);
                if (!success)
                {
                    Debug.WriteLine($"Error  while trying to connect to {appsettings.WifiName}");
                    GoToSleep();
                }

                Debug.WriteLine($"Connected To {appsettings.WifiName}");
            }

            void InitializeAppsettings()
            {
                appsettingsManager = new AppsettingsManager();
                appsettings = appsettingsManager.GetAppsettings();
            }

            void InitializeInterfaceI2C()
            {
                Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);
                Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);
                busIdI2C = 1;
            }

            void InitializeSensorBmp280()
            {

                // TODO
                // implement load config from device twin in IoT Hub 

                var config = new SensorBmp280Config()
                {
                    PressureSampling = Sampling.HighResolution,
                    TemperatureSampling = Sampling.HighResolution,
                    FilteringMode = Bmx280FilteringMode.X2,
                    PressureUnit = "Hectopascal",
                    TemperatureUnit = "DegreeCelsius",
                    StandbyTime = StandbyTime.Ms1000,
                    I2cBusId = 1,
                    I2CAddress = Bmx280Base.SecondaryI2cAddress,
                };

                sensorBmp280 = new SensorBmp280(config); 
            }

            void ConnectIotHub()
            {

                // If you have uploaded the Azure Certificate on the device, just use:
                azureIoT = new(appsettings.IotHubAddress, appsettings.DeviceId, appsettings.DeviceSasKey);

                try
                {
                    azureIoT.Open();
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
}