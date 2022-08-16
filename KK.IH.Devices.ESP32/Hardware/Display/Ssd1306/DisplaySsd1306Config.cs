namespace KK.IH.Devices.ESP32.Hardware.Display.Ssd1306
{

    using Iot.Device.Ssd13xx;
    public class DisplaySsd1306Config
    {
        public int I2cBusId { get; set; }

        public byte I2CAddress { get; set; }

        public Ssd13xx.DisplayResolution DisplayResolution { get; set; }

        public IFont Font { get; set; }
    }
}
