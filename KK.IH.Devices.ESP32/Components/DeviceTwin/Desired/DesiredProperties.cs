namespace KK.IH.Devices.ESP32.Components.DeviceTwin.Desired
{
    using nanoFramework.Json;
    using KK.IH.Devices.ESP32.Hardware.Sensors.Bmp280;
    using KK.IH.Devices.ESP32.Hardware.Sensors.SCD41;
    using nanoFramework.Azure.Devices.Shared;

    public class DesiredProperties
    {
        public string State { get; set; } = string.Empty;

        public int SendInterval { get; set; } = 10000;

        public SensorBmp280ConfigTwin SensorBmp280Config { get; set; } = new SensorBmp280ConfigTwin();

        public SensorScd41ConfigTwin SensorScd41Config { get; set; } = new SensorScd41ConfigTwin();

        public void UpdateFromTwinCollection(TwinCollection twinCollection)
        {
            var deserialized = (DesiredProperties)JsonConvert.DeserializeObject(twinCollection.ToJson(), this.GetType());
            this.State = deserialized.State;
            this.SendInterval = deserialized.SendInterval;
            this.SensorBmp280Config = deserialized.SensorBmp280Config;
            this.SensorScd41Config = deserialized.SensorScd41Config;
        }
    }
}
