using System.Collections;

namespace KK.IH.Devices.ESP32.Hardware.Display
{
    public interface IDisplay
    {
        public void DisplayMeasurement(IList measurementList);

        public void DisplayState();
    }
}
