﻿using System;
using System.Collections;
using Iot.Device.Bmxx80.ReadResult;
using nanoFramework.Json;
using UnitsNet.Units;

namespace KK.IH.Devices.ESP32.Hardware.Sensors.Bmp280
{
    using System.Device.I2c;
    using Iot.Device.Bmxx80;

    class SensorBmp280 : ISensor
    {
        private Bmp280 _sensor;
        private readonly SensorBmp280Config _config;

        public SensorBmp280(SensorBmp280Config config)
        {
            this._config = config;

            this.InitializeSensor();
            this.ConfigureSensor();
        }

        public ISensorResult[] GetMeasurements()
        {
            var sensorReadings = _sensor.Read();
            var sensorResults = new SensorResult[2];

            sensorResults[0] = GetTemperature(sensorReadings);
            sensorResults[1] = GetPressure(sensorReadings);

            return sensorResults;
        }

        private void InitializeSensor()
        {
            var i2cSettings = new I2cConnectionSettings(1, Bmx280Base.SecondaryI2cAddress);
            var i2cDevice = I2cDevice.Create(i2cSettings);
            this._sensor = new Bmp280(i2cDevice);
        }

        private void ConfigureSensor()
        {
            _sensor.FilterMode = this._config.FilteringMode;
            _sensor.StandbyTime = this._config.StandbyTime;
            _sensor.TemperatureSampling = this._config.TemperatureSampling;
            _sensor.PressureSampling = this._config.PressureSampling;
        }

        private SensorResult GetTemperature(Bmp280ReadResult result)
        {
            var kind = "Temperature";
            var value = result.Temperature.As(this.ParseTemperatureUnit(this._config.TemperatureUnit));
            var unit = this._config.TemperatureUnit;

            return new SensorResult(kind, value, unit);
        }

        private SensorResult GetPressure(Bmp280ReadResult result)
        {
            var kind = "Pressure";
            var value = result.Pressure.As(this.ParsePressureUnit(this._config.PressureUnit));
            var unit = this._config.PressureUnit;
   
            return new SensorResult(kind, value, unit);
        }

        private TemperatureUnit ParseTemperatureUnit(string unitName)
        {
            switch (unitName)
            {
                case "Undefined": return TemperatureUnit.DegreeCelsius;
                case "DegreeCelsius": return TemperatureUnit.DegreeCelsius;
                case "DegreeDelisle": return TemperatureUnit.DegreeDelisle;
                case "DegreeFahrenheit": return TemperatureUnit.DegreeFahrenheit;
                case "DegreeNewton": return TemperatureUnit.DegreeNewton;
                case "DegreeRankine": return TemperatureUnit.DegreeRankine;
                case "DegreeReaumur": return TemperatureUnit.DegreeReaumur;
                case "DegreeRoemer": return TemperatureUnit.DegreeRoemer;
                case "Kelvin": return TemperatureUnit.Kelvin;
                case "MillidegreeCelsius": return TemperatureUnit.MillidegreeCelsius;
                case "SolarTemperature": return TemperatureUnit.SolarTemperature;
                default: return TemperatureUnit.Undefined;
            }
        }

        private PressureUnit ParsePressureUnit(string unitName)
        {
            switch (unitName)
            {
                case "Undefined": return PressureUnit.Undefined;
                case "Atmosphere": return PressureUnit.Atmosphere;
                case "Bar": return PressureUnit.Bar;
                case "Centibar": return PressureUnit.Centibar;
                case "Decapascal": return PressureUnit.Decapascal;
                case "Decibar": return PressureUnit.Decibar;
                case "DynePerSquareCentimeter": return PressureUnit.DynePerSquareCentimeter;
                case "FootOfElevation": return PressureUnit.FootOfElevation;
                case "FootOfHead": return PressureUnit.FootOfHead;
                case "Gigapascal": return PressureUnit.Gigapascal;
                case "Hectopascal": return PressureUnit.Hectopascal;
                case "InchOfMercury": return PressureUnit.InchOfMercury;
                case "InchOfWaterColumn": return PressureUnit.InchOfWaterColumn;
                case "Kilobar": return PressureUnit.Kilobar;
                case "KilogramForcePerSquareCentimeter": return PressureUnit.KilogramForcePerSquareCentimeter;
                case "KilogramForcePerSquareMeter": return PressureUnit.KilogramForcePerSquareMeter;
                case "KilogramForcePerSquareMillimeter": return PressureUnit.KilogramForcePerSquareMillimeter;
                case "KilonewtonPerSquareCentimeter": return PressureUnit.KilonewtonPerSquareCentimeter;
                case "KilonewtonPerSquareMeter": return PressureUnit.KilonewtonPerSquareMeter;
                case "KilonewtonPerSquareMillimeter": return PressureUnit.KilonewtonPerSquareMillimeter;
                case "Kilopascal": return PressureUnit.Kilopascal;
                case "KilopoundForcePerSquareFoot": return PressureUnit.KilopoundForcePerSquareFoot;
                case "KilopoundForcePerSquareInch": return PressureUnit.KilopoundForcePerSquareInch;
                case "KilopoundForcePerSquareMil": return PressureUnit.KilopoundForcePerSquareMil;
                case "Megabar": return PressureUnit.Megabar;
                case "MeganewtonPerSquareMeter": return PressureUnit.MeganewtonPerSquareMeter;
                case "Megapascal": return PressureUnit.Megapascal;
                case "MeterOfElevation": return PressureUnit.MeterOfElevation;
                case "MeterOfHead": return PressureUnit.MeterOfHead;
                case "Microbar": return PressureUnit.Microbar;
                case "Micropascal": return PressureUnit.Micropascal;
                case "Millibar": return PressureUnit.Millibar;
                case "MillimeterOfMercury": return PressureUnit.MillimeterOfMercury;
                case "MillimeterOfWaterColumn": return PressureUnit.MillimeterOfWaterColumn;
                case "Millipascal": return PressureUnit.Millipascal;
                case "NewtonPerSquareCentimeter": return PressureUnit.NewtonPerSquareCentimeter;
                case "NewtonPerSquareMeter": return PressureUnit.NewtonPerSquareMeter;
                case "NewtonPerSquareMillimeter": return PressureUnit.NewtonPerSquareMillimeter;
                case "Pascal": return PressureUnit.Pascal;
                case "PoundForcePerSquareFoot": return PressureUnit.PoundForcePerSquareFoot;
                case "PoundForcePerSquareInch": return PressureUnit.PoundForcePerSquareInch;
                case "PoundForcePerSquareMil": return PressureUnit.PoundForcePerSquareMil;
                case "PoundPerInchSecondSquared": return PressureUnit.PoundPerInchSecondSquared;
                case "TechnicalAtmosphere": return PressureUnit.TechnicalAtmosphere;
                case "TonneForcePerSquareCentimeter": return PressureUnit.TonneForcePerSquareCentimeter;
                case "TonneForcePerSquareMeter": return PressureUnit.TonneForcePerSquareMeter;
                case "TonneForcePerSquareMillimeter": return PressureUnit.TonneForcePerSquareMillimeter;
                case "Torr": return PressureUnit.Torr;
                default: return PressureUnit.Undefined;
            }
        }
    }
}