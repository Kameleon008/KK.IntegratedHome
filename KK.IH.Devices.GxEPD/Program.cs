using System;
using System.Device.Spi;
using System.Diagnostics;
using System.Threading;

namespace KK.IH.Devices.GxEPD
{
    public class Program
    {
        public static void Main()
        {
            Debug.WriteLine("Hello from nanoFramework!");

            Thread.Sleep(Timeout.Infinite);

            SpiDevice spiDevice;
            SpiConnectionSettings spiConnectionSettings;

            spiConnectionSettings = new SpiConnectionSettings(1, 5);
            spiDevice = SpiDevice.Create(spiConnectionSettings);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }
    }
}
