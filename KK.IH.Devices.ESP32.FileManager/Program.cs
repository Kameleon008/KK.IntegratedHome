namespace KK.IH.Devices.ESP32.FileManager
{
    using System.IO;
    using System.Text;
    using nanoFramework.Json;
    using KK.IH.Devices.ESP32.FileManager.Models;
    using KK.IH.Devices.ESP32.Utility.Debug;

    public class Program
    {

        public static void Main()
        {

            string fileName = $"appsettings.json";
            string filePath = $"I:\\{fileName}";

            Logger.Info($"Serializing file \"{fileName}\"");
            var appsettings = new AppsettingsModel()
            {
                WifiName = "{WifiName}",
                WifiPassword = "{WifiPassword}",
                DeviceId = "{DeviceId}",
                DeviceSasKey = "{DeviceSasKey}",
                IotHubAddress = "{IotHubAddress}",
            };

            string json = JsonConvert.SerializeObject(appsettings);
            Logger.Info($"Serialized content: {json}");


            Logger.Info($"Writing content to \"{filePath}\"");
            File.Create(filePath);
            byte[] sampleBuffer = Encoding.UTF8.GetBytes(json);
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            fs.Write(sampleBuffer, 0, sampleBuffer.Length);
            fs.Close();



            Logger.Info($"Reading content from \"{filePath}\"");
            FileStream fs2 = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] fileContentBytes = new byte[fs2.Length];
            fs2.Read(fileContentBytes, 0, (int)fs2.Length);
            var fileContentString = Encoding.UTF8.GetString(fileContentBytes, 0, (int)fs2.Length);
            Logger.Info($"Content of readed file: {fileContentString}");

        }
    }
}
