using System.IO;
using System.Text;
using nanoFramework.Json;

namespace KK.IH.Devices.ESP32.Components.Appsettings
{
    public static class AppsettingsManager
    {

        private static string appsettingsPath = "I:\\appsettings.json";

        public static void GetAppsettings(out Appsettings results)
        {
            FileStream fileStream = new FileStream(appsettingsPath, FileMode.Open, FileAccess.Read);
            byte[] fileContentBytes = new byte[fileStream.Length];
            fileStream.Read(fileContentBytes, 0, (int)fileStream.Length);
            var fileContentString = Encoding.UTF8.GetString(fileContentBytes, 0, (int)fileStream.Length);
            results = (Appsettings)JsonConvert.DeserializeObject(fileContentString, typeof(Appsettings));
        }
    }
}
