using System;
using System.Collections.Generic;
using System.Text;

namespace KK.IH.Devices.ESP32.Hardware.Sensors
{
    class SensorResult : ISensorResult
    {
        public SensorResult(string kind, double value, string unit)
        {
            this.Kind = kind;
            this.Value = value;
            this.Unit = unit;
        }

        public string Kind { get; set; }

        public double Value { get; set; }

        public string Unit { get; set; }
    }
}
