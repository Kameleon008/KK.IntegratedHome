namespace KK.IH.Devices.ESP32.Hardware.Sensors.SCD41
{
    using KK.IH.Devices.ESP32.Components.DeviceTwin.Desired;
    using KK.IH.Devices.ESP32.Utility.Debug;
    using KK.IH.Devices.ESP32.Utility.Devices.Scd41;
    using nanoFramework.Json;
    using System.Collections;
    using System.Device.I2c;
    using UnitsNet.Units;

    public class SensorScd41 : ISensor, IDesiredPropertiesSubscriber
    {
        private Scd41 _sensor;
        private SensorScd41Config _config;

        public SensorScd41(DesiredProperties desired)
        {
            this._config = SensorScd41Config.ConvertFromTwin(desired.SensorScd41Config);
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

        public void UpdateDesiredProperties(DesiredProperties desiredProperties)
        {
            var newConfiguration = SensorScd41Config.ConvertFromTwin(desiredProperties.SensorScd41Config);
            this._config = newConfiguration;
            Logger.Info($"Updated config of {this.GetType().Name} with {JsonConvert.SerializeObject(desiredProperties.SensorScd41Config)}");
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
            var category = "C2";
            var co2 = new UnitsNet.VolumeConcentration(result.Co2.Value, VolumeConcentrationUnit.PartPerMillion);
            var value = co2.As(this.ParseCo2Unit(this._config.Co2Unit));
            var unit = this._config.Co2Unit;

            return new SensorResult(category, value, unit);
        }
        private SensorResult GetHumidity(Scd41ReadResult result)
        {
            var catergory = "H2";
            var humidity = new UnitsNet.RelativeHumidity(result.Humidity.Value, RelativeHumidityUnit.Percent);
            var value = humidity.As(this.ParseHumidityUnit(this._config.HumidityUnit));
            var unit = this._config.HumidityUnit;

            return new SensorResult(catergory, value, unit);
        }

        private SensorResult GetTemperature(Scd41ReadResult result)
        {
            var category = "T2";
            var temperature = new UnitsNet.Temperature(result.Temperature.Value, TemperatureUnit.DegreeCelsius);
            var value = temperature.As(this.ParseTemperatureUnit(this._config.TemperatureUnit));
            var unit = this._config.TemperatureUnit;

            return new SensorResult(category, value, unit);
        }

        private TemperatureUnit ParseTemperatureUnit(string unitName)
        {
            switch (unitName)
            {
                case "Undefined": return TemperatureUnit.DegreeCelsius;
                case "DegreeCelsius": return TemperatureUnit.DegreeCelsius;
                case "DegreeDelisle": return TemperatureUnit.DegreeDelisle;
                case "DegreeFahrenheit": return TemperatureUnit.DegreeFahrenheit;
                case "DegreeNewton": return TemperatureUnit.DegreeNewton;
                case "DegreeRankine": return TemperatureUnit.DegreeRankine;
                case "DegreeReaumur": return TemperatureUnit.DegreeReaumur;
                case "DegreeRoemer": return TemperatureUnit.DegreeRoemer;
                case "Kelvin": return TemperatureUnit.Kelvin;
                case "MillidegreeCelsius": return TemperatureUnit.MillidegreeCelsius;
                case "SolarTemperature": return TemperatureUnit.SolarTemperature;
                default: return TemperatureUnit.Undefined;
            }
        }

        private VolumeConcentrationUnit ParseCo2Unit(string unitName)
        {
            switch (unitName)
            {
                case "Undefined": return VolumeConcentrationUnit.Undefined;
                case "CentilitersPerLiter": return VolumeConcentrationUnit.CentilitersPerLiter;
                case "CentilitersPerMililiter": return VolumeConcentrationUnit.CentilitersPerMililiter;
                case "DecilitersPerLiter": return VolumeConcentrationUnit.DecilitersPerLiter;
                case "DecilitersPerMililiter": return VolumeConcentrationUnit.DecilitersPerMililiter;
                case "DecimalFraction": return VolumeConcentrationUnit.DecimalFraction;
                case "LitersPerLiter": return VolumeConcentrationUnit.LitersPerLiter;
                case "LitersPerMililiter": return VolumeConcentrationUnit.LitersPerMililiter;
                case "MicrolitersPerLiter": return VolumeConcentrationUnit.MicrolitersPerLiter;
                case "MicrolitersPerMililiter": return VolumeConcentrationUnit.MicrolitersPerMililiter;
                case "MillilitersPerLiter": return VolumeConcentrationUnit.MillilitersPerLiter;
                case "MillilitersPerMililiter": return VolumeConcentrationUnit.MillilitersPerMililiter;
                case "NanolitersPerLiter": return VolumeConcentrationUnit.NanolitersPerLiter;
                case "NanolitersPerMililiter": return VolumeConcentrationUnit.NanolitersPerMililiter;
                case "PartPerBillion": return VolumeConcentrationUnit.PartPerBillion;
                case "PartPerMillion": return VolumeConcentrationUnit.PartPerMillion;
                case "PartPerThousand": return VolumeConcentrationUnit.PartPerThousand;
                case "PartPerTrillion": return VolumeConcentrationUnit.PartPerTrillion;
                case "Percent": return VolumeConcentrationUnit.Percent;
                case "PicolitersPerLiter": return VolumeConcentrationUnit.PicolitersPerLiter;
                case "PicolitersPerMililiter": return VolumeConcentrationUnit.PicolitersPerMililiter;
                default: return VolumeConcentrationUnit.Undefined;
            }
        }

        private RelativeHumidityUnit ParseHumidityUnit(string unitName)
        {
            switch (unitName)
            {
                case "Undefined": return RelativeHumidityUnit.Undefined;
                case "Percent": return RelativeHumidityUnit.Percent;
                default: return RelativeHumidityUnit.Undefined;
            }
        }


    }
}
