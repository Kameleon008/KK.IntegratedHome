using System;
using System.Collections.Generic;
using System.Text;

namespace KK.IH.Devices.ESP32.Hardware.Sensors.SCD41
{
    public class SensorScd41Config
    {
        public int I2cBusId { get; set; }

        public byte I2CAddress { get; set; }
    }
}
