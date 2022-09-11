namespace KK.IH.Devices.ESP32.Hardware.Sensors.Bmp280
{
    public class SensorBmp280ConfigTwin
    {
        public string StandbyTime { get; set; }

        public string FilteringMode { get; set; }

        public string TemperatureSampling { get; set; }

        public string PressureSampling { get; set; }

        public string TemperatureUnit { get; set; }

        public string PressureUnit { get; set; }

        public string I2cBusId { get; set; }

        public string I2CAddress { get; set; }
    }
}
