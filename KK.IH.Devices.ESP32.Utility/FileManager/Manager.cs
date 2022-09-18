namespace KK.IH.Devices.ESP32.Utility.FileManager
{
    using System.IO;
    using System.Text;
    using nanoFramework.Json;
    using KK.IH.Devices.ESP32.Utility.Debug;
    using System;

    public static class Manager
    {
        public static void SaveFile(byte[] appsettings, Type type)
        {
            string fileName = $"appsettings.json";
            string filePath = $"I:\\{fileName}";

            Logger.Info($"Serializing file \"{fileName}\"");
            var fromFile = Encoding.UTF8.GetString(appsettings, 0, appsettings.Length);
            var deserialized2 = JsonConvert.DeserializeObject(fromFile.Substring(1), type);
            var byteBuffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(deserialized2));
            
            File.Create(filePath);
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            fs.Write(byteBuffer, 0, byteBuffer.Length);
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
