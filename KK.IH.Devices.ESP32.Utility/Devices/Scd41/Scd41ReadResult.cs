namespace KK.IH.Devices.ESP32.Utility.Devices.Scd41
{
    using KK.IH.Devices.ESP32.Utility.Devices.Scd41.Structs;

    public class Scd41ReadResult
    {
        public Scd41ReadResult(Co2 co2, Humidity humidity, Temperature temperature)
        {
            this.Co2 = co2;
            this.Humidity = humidity;
            this.Temperature = temperature;
        }

        public Co2 Co2 { get; }

        public Humidity Humidity { get; }

        public Temperature Temperature { get; }

    }
}
