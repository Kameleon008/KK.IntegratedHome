namespace KK.IH.Devices.ESP32.Utility.Devices.Scd41.Structs
{
    public struct Temperature
    {
        public Temperature(double value)
        {
            this.Value = value;
        }

        public double Value { get; }
    }
}