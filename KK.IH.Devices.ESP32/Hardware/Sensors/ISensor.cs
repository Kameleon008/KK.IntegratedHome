using System.Collections;

namespace KK.IH.Devices.ESP32.Hardware.Sensors
{
    using System.Collections.Generic;

    public interface ISensor
    {
        public ISensorResult[] GetMeasurements();
    }
}
