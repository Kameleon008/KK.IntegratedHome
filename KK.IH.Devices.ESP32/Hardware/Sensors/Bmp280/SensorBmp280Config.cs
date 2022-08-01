using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.FilteringMode;
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
    }
}
