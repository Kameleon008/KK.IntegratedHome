namespace KK.IH.Devices.ESP32.Utility.Devices.Scd41.Structs
{
    public struct Co2
    {
        public Co2(double value)
        {
            this.Value = value;
        }

        public double Value { get; }
    }
}
