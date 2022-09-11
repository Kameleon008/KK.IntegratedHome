namespace KK.IH.Devices.ESP32
{
    using System.Collections;
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
    using KK.IH.Devices.ESP32.Utility.Debug;
    using KK.IH.Devices.ESP32.Components.DeviceTwin;
    using KK.IH.Devices.ESP32.Components.Device;
    using KK.IH.Devices.ESP32.Components.DeviceTwin.Desired;

    class Program
    {
        static Appsettings appsettings;

        static AppsettingsManager appsettingsManager;

        static DeviceManager deviceManager;

        static DeviceTwinManager deviceTwinManager;

        static IList sensorList = new ArrayList();


        static void Main(string[] args)
        {
            InitHardware();
            InitAppsettings();
            InitComponents();
            InitSensors();

            while (true)
            {
                var readResult = GetMeasurement();

                var messageContent = JsonConvert.SerializeObject(readResult);
                deviceManager.Client.SendMessage(messageContent, new CancellationTokenSource(2000).Token);
                Logger.Info($"Message: {messageContent}");
                Thread.Sleep(deviceTwinManager.DesiredProperties.SendInterval);
            }
        }

        private static void DeviceClient_TwinUpdated(object sender, nanoFramework.Azure.Devices.Shared.TwinUpdateEventArgs updateTwinEvent)
        {
            var twinString = JsonConvert.SerializeObject(updateTwinEvent.Twin);
            Logger.Info(twinString);
        }

        private static void InitHardware()
        {
            Controler.ConfigurePeripherials();
        }

        private static void InitAppsettings()
        {
            appsettingsManager = new AppsettingsManager();
            appsettings = appsettingsManager.GetAppsettings();
        }

        private static void InitComponents()
        {
            deviceManager = new DeviceManager(appsettings);
            deviceTwinManager = new DeviceTwinManager(deviceManager.Client);
        }

        private static void InitSensors()
        {
            sensorList.Add(InitializeSensorBmp280());
            sensorList.Add(InitializeSensorScd41());

            foreach (var sensor in sensorList)
            {
                var subscriber = (IDesiredPropertiesSubscriber)sensor;
                deviceTwinManager.AddDesiredPropertiesSubscriber(subscriber);
            }
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

        static ISensor InitializeSensorBmp280()
        {
            var sensorBmp280 = new SensorBmp280(deviceTwinManager.DesiredProperties.SensorBmp280Config);
            return sensorBmp280;
        }

        static ISensor InitializeSensorScd41()
        {
            var sensorScd41 = new SensorScd41(deviceTwinManager.DesiredProperties.SensorScd41Config);
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