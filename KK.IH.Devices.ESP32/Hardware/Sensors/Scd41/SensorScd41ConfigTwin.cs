namespace KK.IH.Devices.ESP32.Hardware.Sensors.SCD41
{
    public class SensorScd41ConfigTwin
    {
        public string I2cBusId { get; set; }

        public string I2CAddress { get; set; }

        public string TemperatureUnit { get; set; }

        public string Co2Unit { get; set; }

        public string HumidityUnit { get; set; }
    }
}
