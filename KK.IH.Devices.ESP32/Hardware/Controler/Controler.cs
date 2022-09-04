namespace KK.IH.Devices.ESP32.Hardware.Peripherials
{
    using nanoFramework.Hardware.Esp32;
    
    static class Controler
    {


        public static void ConfigurePeripherials()
        {
            ConfigureInterfaceI2C();
        }

        private static void ConfigureInterfaceI2C()
        {
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_DATA);
            Configuration.SetPinFunction(22, DeviceFunction.I2C1_CLOCK);
        }
    }
}
