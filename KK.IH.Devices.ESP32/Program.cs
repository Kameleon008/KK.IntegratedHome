namespace KK.IH.Devices.ESP32
{
    using System.Collections;
    using System.Threading;
    using Components.Appsettings;
    using Hardware.Sensors;
    using Hardware.Peripherials;
    using Providers;
    using nanoFramework.Json;
    using KK.IH.Devices.ESP32.Hardware.Sensors.Bmp280;
    using KK.IH.Devices.ESP32.Hardware.Sensors.SCD41;
    using KK.IH.Devices.ESP32.Utility.Debug;
    using KK.IH.Devices.ESP32.Components.DeviceTwin;
    using KK.IH.Devices.ESP32.Components.Device;
    using KK.IH.Devices.ESP32.Components.DeviceTwin.Desired;

    class Program
    {
        static DeviceManager deviceManager;

        static DeviceTwinManager deviceTwinManager;

        static IList sensorList = new ArrayList();

        static void Main(string[] args)
        {
            InitDevice();
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

        private static void InitDevice()
        {
            AppsettingsManager.GetAppsettings(out var appsettings);
            NetworkProvider.ProvideWifiConnection(appsettings);
            Controler.ConfigureInterfaceI2C();

            deviceManager = new DeviceManager(appsettings);
            deviceTwinManager = new DeviceTwinManager(deviceManager.Client);
        }

        private static void InitSensors()
        {
            AddSensor(new SensorBmp280(deviceTwinManager.DesiredProperties));
            AddSensor(new SensorScd41(deviceTwinManager.DesiredProperties));
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

        private static void AddSensor(ISensor sensor)
        {
            sensorList.Add(sensor);
            var subscriber = (IDesiredPropertiesSubscriber)sensor;
            deviceTwinManager.AddDesiredPropertiesSubscriber(subscriber);
        }
    }
}