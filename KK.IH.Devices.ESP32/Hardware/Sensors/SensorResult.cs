using System;
using System.Collections.Generic;
using System.Text;

namespace KK.IH.Devices.ESP32.Hardware.Sensors
{
    class SensorResult : ISensorResult
    {
        public SensorResult(string category, double value, string unit)
        {
            this.Category = category;
            this.Value = value;
            this.Unit = unit;
        }

        public string Category { get; set; }

        public double Value { get; set; }

        public string Unit { get; set; }
    }
}
