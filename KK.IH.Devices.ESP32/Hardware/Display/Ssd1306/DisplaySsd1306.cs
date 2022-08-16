using System.Collections;
using KK.IH.Devices.ESP32.Hardware.Sensors;

namespace KK.IH.Devices.ESP32.Hardware.Display.Ssd1306
{
    using System;
    using System.Device.I2c;
    using Iot.Device.Ssd13xx;

    public class DisplaySsd1306 : IDisplay
    {
        private Ssd1306 _display;
        private readonly DisplaySsd1306Config _config;

        public DisplaySsd1306(DisplaySsd1306Config config)
        {
            this._config = config;
            this.InitializeDisplay();
            this.ConfigureDisplay();
        }

        public void DisplayMeasurement(IList measurementList)
        {
            int startLineFromPixel = 0;
            foreach (ISensorResult measurement in measurementList)
            {
                //var value = megasurement.Value.ToString("F2");
                _display.DrawString(0, startLineFromPixel, DisplaySsd1306Converter.ConvertToDisplay(measurement), 1, false);
                _display.Display();
                startLineFromPixel += 12;
            }
        }

        public void DisplayState()
        {
            throw new NotImplementedException();
        }

        private void InitializeDisplay()
        {
            var i2CSettings = new I2cConnectionSettings(_config.I2cBusId, _config.I2CAddress);
            var i2CDevice = I2cDevice.Create(i2CSettings);
            this._display = new Ssd1306(i2CDevice, _config.DisplayResolution);
        }

        private void ConfigureDisplay()
        {
            _display.Font = _config.Font;
        }



    }
}
