using System;
using System.Collections.Generic;
using System.Text;

namespace KK.IH.Devices.ESP32.Hardware.Sensors
{
    public interface ISensorResult
    {
        string Category { get; set; }

        double Value { get; set; }

        string Unit { get; set; }

    }
}
