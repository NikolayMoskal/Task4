using System;
using System.Collections.Specialized;
using System.Configuration;
using FileGenerator.Generator.Models;
using NLog;

namespace FileGenerator
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static string _directoryPath;
        private static string _employeeName;
        private static int _linesCount;

        public static void Main(string[] args)
        {
            var appSettings = GetAppSettings();
            if (appSettings == null)
            {
                return;
            }

            ParseAppSettings(appSettings);

            var fileName = new SurnameDateFileName(_employeeName, DateTime.Today, "_");
            try
            {
                var product = GetCustomProduct(appSettings);
                var clientName = GetCustomClientName(appSettings);
                var line = new CsvLine(clientName, product);
                new CsvGenerator(fileName, line).Generate(_directoryPath, _linesCount);
            }
            catch (ArgumentException e)
            {
                Logger.Error($"Wrong argument: {e.Message}. Stack trace: {e.StackTrace}");
            }
        }

        private static NameValueCollection GetAppSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count > 0)
                {
                    return appSettings;
                }

                Logger.Warn("App settings is empty");
                return null;
            }
            catch (ConfigurationErrorsException e)
            {
                Logger.Error($"Error reading app settings: {e.StackTrace}");
                return null;
            }
        }

        private static void ParseAppSettings(NameValueCollection appSettings)
        {
            _directoryPath = appSettings["directoryPath"];
            _employeeName = string.IsNullOrEmpty(appSettings["employeeName"])
                ? "<empty>"
                : appSettings["employeeName"];
            int.TryParse(appSettings["linesCount"], out _linesCount);
            if (_linesCount == 0)
            {
                Logger.Warn("Lines count in CSV file was set to 0. It is correct value?");
            }
        }

        private static string GetCustomClientName(NameValueCollection appSettings)
        {
            var names = appSettings["clientNames"]?.Split(',');
            if (names == null)
            {
                return null;
            }
            
            var index = new Random().Next(0, names.Length);
            return names[index];
        }

        private static Product GetCustomProduct(NameValueCollection appSettings)
        {
            var productNames = appSettings["products"]?.Split(',');
            if (productNames == null)
            {
                return null;
            }

            var index = new Random().Next(0, productNames.Length);
            return new Product(productNames[index]);

        }
    }
}