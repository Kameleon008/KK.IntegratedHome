namespace KK.IH.Devices.ESP32.Hardware.Sensors.SCD41
{
    using System;
    
    public class SensorScd41Config
    {
        public int I2cBusId { get; set; }

        public byte I2CAddress { get; set; }

        public string TemperatureUnit { get; set; }

        public string Co2Unit { get; set; }

        public string HumidityUnit { get; set; }

        public static SensorScd41Config ConvertFromTwin(SensorScd41ConfigTwin twin)
        {
            var result = new SensorScd41Config();

            result.I2cBusId = GetI2cBusId(twin.I2cBusId);
            result.I2CAddress = GetI2CAddress(twin.I2CAddress);
            result.TemperatureUnit = GetTemperatureUnit(twin.TemperatureUnit);
            result.Co2Unit = GetCo2Unit(twin.Co2Unit);
            result.HumidityUnit = GetHumidityUnit(twin.HumidityUnit);

            return result;
        }

        private static int GetI2cBusId(string value)
        {
            return Convert.ToInt32(value);
        }

        private static byte GetI2CAddress(string value)
        {
            return Convert.ToByte(value);
        }

        private static string GetTemperatureUnit(string value)
        {
            return value;
        }

        private static string GetCo2Unit(string value)
        {
            return value;
        }

        private static string GetHumidityUnit(string value)
        {
            return value;
        }
    }
}
