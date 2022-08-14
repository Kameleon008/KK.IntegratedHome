using System;
using System.Device.I2c;
using System.Diagnostics;
using System.Threading;

namespace KK.IH.Devices.ESP32.Utility.Devices.Scd41
{
    public class Scd41
    {
        I2cDevice _device;

        public Scd41(I2cDevice device)
        {
            this._device = device;
        }

        public void GetSerialNumber()
        {
            byte[] payload = BitConverter.GetBytes(60421);
            System.SpanByte buffer = new System.SpanByte(payload);
            this._device.Write(buffer);

            Thread.Sleep(100);

            byte[] received = new byte[9];
            this._device.Read(received);

            Debug.WriteLine(received.ToString());
        }
    }
}
