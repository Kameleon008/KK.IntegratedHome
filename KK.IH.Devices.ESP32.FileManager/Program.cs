namespace KK.IH.Devices.ESP32.FileManager
{
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using nanoFramework.Json;
    using KK.IH.Devices.ESP32.FileManager.Models;

    public class Program
    {

        public static void Main()
        {

            string sampleFileInternalPath = "I:\\appsettings.json";

            Debug.WriteLine("Serializing File");
            var appsettings = new AppsettingsModel()
            {
                WifiName = "{WifiName}",
                WifiPassword = "{WifiPassword}",
                DeviceId = "{DeviceId}",
                DeviceSasKey = "{DeviceSasKey}",
                IotHubAddress = "{IotHubAddress}",
            };

            string json = JsonConvert.SerializeObject(appsettings);
            Debug.WriteLine(json);


            Debug.WriteLine("Writing File");
            File.Create(sampleFileInternalPath);
            byte[] sampleBuffer = Encoding.UTF8.GetBytes(json);
            FileStream fs = new FileStream(sampleFileInternalPath, FileMode.Open, FileAccess.ReadWrite);
            fs.Write(sampleBuffer, 0, sampleBuffer.Length);
            fs.Close();



            Debug.WriteLine("Reading File");
            FileStream fs2 = new FileStream(sampleFileInternalPath, FileMode.Open, FileAccess.Read);
            byte[] fileContentBytes = new byte[fs2.Length];
            fs2.Read(fileContentBytes, 0, (int)fs2.Length);
            var fileContentString = Encoding.UTF8.GetString(fileContentBytes, 0, (int)fs2.Length);
            Debug.WriteLine(fileContentString);

        }
    }
}
