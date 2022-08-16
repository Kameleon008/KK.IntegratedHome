using System.IO;
using System.Text;
using nanoFramework.Json;

namespace KK.IH.Devices.ESP32.Components.Appsettings
{
    using System;

    public class AppsettingsManager: IAppsettingsManager
    {

        private readonly string appsettingsPath;

        public AppsettingsManager()
        {
            this.appsettingsPath = "I:\\appsettings.json";
        }

        public AppsettingsManager(string pathToAppsettingsFile)
        {
            this.appsettingsPath = pathToAppsettingsFile;
        }

        public Appsettings GetAppsettings()
        {
            FileStream fileStream = new FileStream(appsettingsPath, FileMode.Open, FileAccess.Read);
            byte[] fileContentBytes = new byte[fileStream.Length];
            fileStream.Read(fileContentBytes, 0, (int)fileStream.Length);
            var fileContentString = Encoding.UTF8.GetString(fileContentBytes, 0, (int)fileStream.Length);
            return (Appsettings)JsonConvert.DeserializeObject(fileContentString, typeof(Appsettings));
        }

        public bool SaveAppsettings(Appsettings appsettings)
        {
            return true;
        }
    }
}
