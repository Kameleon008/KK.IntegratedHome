using KK.IH.Devices.ESP32.Utility.Devices.Scd41;
using System.Collections;
using System.Device.I2c;

namespace KK.IH.Devices.ESP32.Hardware.Sensors.SCD41
{
    public class SensorScd41 : ISensor
    {
        private Scd41 _sensor;
        private readonly SensorScd41Config _config;

        public SensorScd41(SensorScd41Config config)
        {
            this._config = config;
            this.InitializeSensor();
        }

        public IList GetMeasurements()
        {
            var sensorReadings = _sensor.Read();

            IList resultList = new ArrayList();

            this.PrepareResultCo2(ref resultList, sensorReadings);
            this.PrepareResultHumidity(ref resultList, sensorReadings);
            this.PrepareResultTemperature(ref resultList, sensorReadings);

            return resultList;
        }

        private void InitializeSensor()
        {
            var i2cSettings = new I2cConnectionSettings(_config.I2cBusId, _config.I2CAddress);
            var i2cDevice = I2cDevice.Create(i2cSettings);
            this._sensor = new Scd41(i2cDevice);
        }

        private void PrepareResultCo2(ref IList resultList, Scd41ReadResult result)
        {
            if (result.Co2.Value != 0)
            {
                resultList.Add(GetCo2(result));
            }
        }

        private void PrepareResultHumidity(ref IList resultList, Scd41ReadResult result)
        {
            if (result.Humidity.Value != 0)
            {
                resultList.Add(GetHumidity(result));
            }
        }

        private void PrepareResultTemperature(ref IList resultList, Scd41ReadResult result)
        {
            if (result.Temperature.Value != -45)
            {
                resultList.Add(GetTemperature(result));
            }
        }

        private SensorResult GetCo2(Scd41ReadResult result)
        {
            var kind = "C2";
            var value = result.Co2.Value;
            var unit = "ParticlesPerMillion";

            return new SensorResult(kind, value, unit);
        }
        private SensorResult GetHumidity(Scd41ReadResult result)
        {
            var kind = "H2";
            var value = result.Humidity.Value;
            var unit = "Percentage";

            return new SensorResult(kind, value, unit);
        }

        private SensorResult GetTemperature(Scd41ReadResult result)
        {
            var category = "T2";
            var value = result.Temperature.Value;
            var unit = "DegreeCelsius";

            return new SensorResult(category, value, unit);
        }
    }
}
