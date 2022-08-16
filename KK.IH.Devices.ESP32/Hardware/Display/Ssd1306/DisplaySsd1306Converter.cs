using KK.IH.Devices.ESP32.Hardware.Sensors;

namespace KK.IH.Devices.ESP32.Hardware.Display.Ssd1306
{
    public static class DisplaySsd1306Converter
    {

        public static string ConvertToDisplay(ISensorResult measurement)
        {
            var unit = DisplaySsd1306Converter.ParseUnit(measurement.Unit);
            return $@"{measurement.Category}: {measurement.Value.ToString("F2")} {unit}";
        }

        private static string ParseUnit(string unit)
        {
            switch (unit)
            {
                case "ParticlesPerMillion": return "[ppm]";
                case "Percentage": return "[%]";
                case "DegreeCelsius": return "[*C]";
                case "Hectopascal": return "[hPa]";
                default: return unit;
            }
        }
    }
}
