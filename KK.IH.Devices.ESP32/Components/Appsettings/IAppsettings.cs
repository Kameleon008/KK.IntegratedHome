using System;
using System.Collections.Generic;
using System.Text;

namespace KK.IH.Devices.ESP32.Components.Appsettings
{
    interface IAppsettings
    {
        public string IotHubAddress { get; set; }

        public string DeviceId { get; set; }

        public string DeviceSasKey { get; set; }

        public string WifiName { get; set; }

        public string WifiPassword { get; set; }
    }
}
