using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.FilteringMode;
using System;
using UnitsNet.Units;

namespace KK.IH.Devices.ESP32.Hardware.Sensors.Bmp280
{
    public class SensorBmp280Config
    {
        public StandbyTime StandbyTime { get; set; }

        public Bmx280FilteringMode FilteringMode { get; set; }

        public Sampling TemperatureSampling { get; set; }

        public Sampling PressureSampling { get; set; }

        public string TemperatureUnit { get; set; }

        public string PressureUnit { get; set; }

        public int I2cBusId { get; set; }

        public byte I2CAddress { get; set; }

        public static SensorBmp280Config ConvertFromTwin(SensorBmp280ConfigTwin twin)
        {
            var result = new SensorBmp280Config();

            result.StandbyTime = GetStandbyTime(twin.StandbyTime);
            result.FilteringMode = GetFilteringMode(twin.FilteringMode);
            result.TemperatureSampling = GetSampling(twin.TemperatureSampling);
            result.PressureSampling = GetSampling(twin.PressureSampling);
            result.TemperatureUnit = GetTemperatureUnit(twin.TemperatureUnit);
            result.PressureUnit = GetPressureUnit(twin.PressureUnit);
            result.I2cBusId = GetI2cBusId(twin.I2cBusId);
            result.I2CAddress = GetI2CAddress(twin.I2CAddress);

            return result;
        }



        private static StandbyTime GetStandbyTime(string value)
        {
            switch (value)
            {
                case "Ms0_5": return StandbyTime.Ms0_5;
                case "Ms10": return StandbyTime.Ms10;
                case "Ms20": return StandbyTime.Ms20;
                case "Ms62_5": return StandbyTime.Ms62_5;
                case "Ms125": return StandbyTime.Ms125;
                case "Ms250": return StandbyTime.Ms250;
                case "Ms500": return StandbyTime.Ms500;
                case "Ms1000": return StandbyTime.Ms1000;
                default: return StandbyTime.Ms1000;
            }
        }

        private static Bmx280FilteringMode GetFilteringMode(string value)
        {
            switch (value)
            {
                case "Off": return Bmx280FilteringMode.Off;
                case "X2": return Bmx280FilteringMode.X2;
                case "X4": return Bmx280FilteringMode.X4;
                case "X8": return Bmx280FilteringMode.X8;
                case "X16": return Bmx280FilteringMode.X16;
                default: return Bmx280FilteringMode.Off;

            }
        }

        private static Sampling GetSampling(string value)
        {
            switch (value)
            {
                case "Skipped": return Sampling.Skipped;
                case "UltraLowPower": return Sampling.UltraLowPower;
                case "LowPower": return Sampling.LowPower;
                case "Standard": return Sampling.Standard;
                case "HighResolution": return Sampling.HighResolution;
                case "UltraHighResolution": return Sampling.UltraHighResolution;
                default: return Sampling.Skipped;
            }
        }

        private static string GetTemperatureUnit(string value)
        {
            return value;
        }

        private static string GetPressureUnit(string value)
        {
            return value;
        }

        private static int GetI2cBusId(string value)
        {
            return Convert.ToInt32(value);
        }

        private static byte GetI2CAddress(string value)
        {
            switch (value)
            {
                case "Default": return Bmx280Base.DefaultI2cAddress;
                case "Secondary": return Bmx280Base.SecondaryI2cAddress;
                default: return Bmx280Base.DefaultI2cAddress;
            }
        }
    }
}
