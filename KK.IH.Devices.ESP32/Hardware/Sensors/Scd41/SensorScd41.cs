using KK.IH.Devices.ESP32.Utility.Devices.Scd41;
using System;
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
            throw new NotImplementedException();
        }

        private void InitializeSensor()
        {
            var i2cSettings = new I2cConnectionSettings(_config.I2cBusId, _config.I2CAddress);
            var i2cDevice = I2cDevice.Create(i2cSettings);
            this._sensor = new Scd41(i2cDevice);
        }

        public void GetSerialNumber()
        {
            this._sensor.GetSerialNumber();
        }

    }
}
