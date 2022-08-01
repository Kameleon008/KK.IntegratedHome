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
            int secondsToGoToSleep = 2;
            int busIdI2C;

            IAppsettings appsettings;
            IAppsettingsManager appsettingsManager;

            Bmp280 bmp280;
            DeviceClient azureIoT;

            InitializeAppsettings();
            InitializeInterfaceI2C();
            InitializeSensorBmp280();
            ConnectToWifi();
            ConnectIotHub();


            while (true)
            {

                var readResult = bmp280.Read();
                string messageContent = $"{{\"Temperature\":{readResult.Temperature.DegreesCelsius},\"Pressure\":{readResult.Pressure.Hectopascals}}}";
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
                I2cConnectionSettings i2cSettings = new(busIdI2C, Bmp280.SecondaryI2cAddress);
                I2cDevice i2cDevice = I2cDevice.Create(i2cSettings);
                bmp280 = new Bmp280(i2cDevice);

                bmp280.TemperatureSampling = Sampling.HighResolution;
                bmp280.PressureSampling = Sampling.HighResolution;
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