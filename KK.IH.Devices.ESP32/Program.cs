namespace KK.IH.Devices.ESP32
{
    using System.Collections;
    using System.Diagnostics;
    using System.Threading;
    using Components.Appsettings;
    using Hardware.Sensors;
    using Hardware.Peripherials;
    using Providers;
    using nanoFramework.Json;
    using nanoFramework.Azure.Devices.Client;
    using KK.IH.Devices.ESP32.Hardware.Sensors.Bmp280;
    using KK.IH.Devices.ESP32.Hardware.Sensors.SCD41;
    using Iot.Device.Bmxx80;
    using Iot.Device.Bmxx80.FilteringMode;

    class Program
    {
        static IAppsettings appsettings;
        static IAppsettingsManager appsettingsManager;

        static DeviceClient deviceClient;

        static IList sensorList;


        static void Main(string[] args)
        {
            Init();

            sensorList = new ArrayList();
            sensorList.Add(InitializeSensorBmp280());
            sensorList.Add(InitializeSensorScd41());

            while (true)
            {
                var readResult = GetMeasurement();

                var messageContent = JsonConvert.SerializeObject(readResult);
                //deviceClient.SendMessage(messageContent, new CancellationTokenSource(2000).Token);
                Debug.WriteLine(messageContent);

                Thread.Sleep(5 * 1000);
            }

        }

        static void Init()
        {
            appsettingsManager = new AppsettingsManager();
            appsettings = appsettingsManager.GetAppsettings();

            NetworkProvider.ProvideWifiConnection(appsettings);
            ClientProvider.ProvideIotHubConnection(appsettings, ref deviceClient);
            Controler.ConfigurePeripherials();
        }


        static IList GetMeasurement()
        {
            IList readResult = new ArrayList();
            foreach (ISensor sensor in sensorList)
            {
                var sensorMeasurement = sensor.GetMeasurements();
                foreach (ISensorResult measurement in sensorMeasurement)
                {
                    readResult.Add(measurement);
                }
            }

            return readResult;
        }

        //void InitializeInterfaceI2C()
        //{
        //    Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);
        //    Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);
        //}

        static ISensor InitializeSensorBmp280()
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

            var sensorBmp280 = new SensorBmp280(config);
            return sensorBmp280;
        }

        static ISensor InitializeSensorScd41()
        {
            var config = new SensorScd41Config()
            {
                I2cBusId = 1,
                I2CAddress = 0x62,
            };

            var sensorScd41 = new SensorScd41(config);
            return sensorScd41;
        }

        //void InitializeDisplaySsd1306()
        //{
        //    var config = new DisplaySsd1306Config()
        //    {
        //        I2cBusId = 1,
        //        I2CAddress = Ssd1306.DefaultI2cAddress,
        //        Font = new BasicFont(),
        //        DisplayResolution = Ssd13xx.DisplayResolution.OLED128x64
        //    };

        //    //displaySsd1306 = new DisplaySsd1306(config);
        //}
    }
}