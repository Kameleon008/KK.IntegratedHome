namespace KK.IH.Devices.ESP32.Hardware.Sensors
{
    using System.Collections;

    public interface ISensor
    {
        public IList GetMeasurements();
    }
}
