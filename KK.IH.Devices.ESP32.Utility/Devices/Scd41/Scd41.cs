namespace KK.IH.Devices.ESP32.Utility.Devices.Scd41
{
    using KK.IH.Devices.ESP32.Utility.Devices.Scd41.Structs;
    using System;
    using System.Device.I2c;
    using System.Diagnostics;
    using System.Threading;

    public class Scd41
    {
        private readonly I2cDevice _device;

        public Scd41(I2cDevice device)
        {
            this._device = device;
            this.StartPeriodicMeasurement();
            Thread.Sleep(5000);
        }

        public Scd41ReadResult Read()
        {
            var writeBufferArray = BitConverter.GetBytes(0xEC05);
            var writeBuffer = new SpanByte(writeBufferArray);
  
            var writeResult = _device.Write(writeBuffer);
            Debug.WriteLine($"Read(): Write: {writeResult.BytesTransferred}, {writeResult.Status}");

            var readBufferArray = new byte[12];
            var readBuffer = new SpanByte(readBufferArray);

            var readResult = _device.Read(readBuffer);
            Debug.WriteLine($"Read(): Read: {readResult.BytesTransferred}, {readResult.Status}");

            var co2 = this.GetCo2FromReadings(readBuffer);
            var humidity = this.GetHumidityFromReadings(readBuffer);
            var temperature = this.GetTemperatureFromReadings(readBuffer);

            return new Scd41ReadResult(co2, humidity, temperature);
        }

        private Co2 GetCo2FromReadings(SpanByte buffer)
        {
            var msb = (uint)buffer[0];
            var lsb = (uint)buffer[1];
            var value = msb * 256 + lsb;
            var co2 = new Co2(value);
            return co2;
        }
        private Humidity GetHumidityFromReadings(SpanByte buffer)
        {
            var msb = (uint)buffer[6];
            var lsb = (uint)buffer[7];
            double value = 100 * ((double)msb * 256 + (double)lsb) / 65536;
            var humidity = new Humidity(value);
            return humidity;
        }

        private Temperature GetTemperatureFromReadings(SpanByte buffer)
        {
            var msb = (uint)buffer[3];
            var lsb = (uint)buffer[4];
            double value = -45 + 175 * ((double)msb * 256 + (double)lsb) / 65536;
            var temperature = new Temperature(value);
            return temperature;
        }

        private void StartPeriodicMeasurement()
        {
            var writeBufferArray = new byte[2];
            writeBufferArray[0] = 0x21;
            writeBufferArray[1] = 0xB1;
            var writeBuffer = new SpanByte(writeBufferArray);
            var result = this._device.Write(writeBuffer);
            Debug.WriteLine($"StartPeriodicMeasurement(): Write: {result.BytesTransferred}, {result.Status}");

        }
    }
}
