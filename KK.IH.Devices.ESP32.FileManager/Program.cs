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

            string sampleFileInternalPath = "I:\\appsettings.json";

            Logger.Info("Serializing File");
            var appsettings = new AppsettingsModel()
            {
                WifiName = "{WifiName}",
                WifiPassword = "{WifiPassword}",
                DeviceId = "{DeviceId}",
                DeviceSasKey = "{DeviceSasKey}",
                IotHubAddress = "{IotHubAddress}",
            };

            string json = JsonConvert.SerializeObject(appsettings);
            Logger.Info(json);


            Logger.Info("Writing File");
            File.Create(sampleFileInternalPath);
            byte[] sampleBuffer = Encoding.UTF8.GetBytes(json);
            FileStream fs = new FileStream(sampleFileInternalPath, FileMode.Open, FileAccess.ReadWrite);
            fs.Write(sampleBuffer, 0, sampleBuffer.Length);
            fs.Close();



            Logger.Info("Reading File");
            FileStream fs2 = new FileStream(sampleFileInternalPath, FileMode.Open, FileAccess.Read);
            byte[] fileContentBytes = new byte[fs2.Length];
            fs2.Read(fileContentBytes, 0, (int)fs2.Length);
            var fileContentString = Encoding.UTF8.GetString(fileContentBytes, 0, (int)fs2.Length);
            Logger.Info(fileContentString);

        }
    }
}
