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
            var twinColectionSerialized = this.Serialize(twinCollection);
            var deserialized = this.Deserialize(twinColectionSerialized);
            var casted = this.Convert(deserialized);

            this.State = casted.State;
            this.SendInterval = casted.SendInterval;
            this.SensorBmp280Config = casted.SensorBmp280Config;
            this.SensorScd41Config = casted.SensorScd41Config;
        }

        private string Serialize(TwinCollection twinCollection)
        {
            var result = twinCollection.ToJson();
            return result;
        }

        private object Deserialize(string toDeserialize)
        {
            var result = JsonConvert.DeserializeObject(toDeserialize, this.GetType());
            return result;
        }

        private DesiredProperties Convert(object toConvert)
        {
            var result = (DesiredProperties)toConvert;
            return result;
        }
    }
}
