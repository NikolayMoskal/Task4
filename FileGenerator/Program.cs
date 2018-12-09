using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Threading;
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
                var products = GetCustomProduct(appSettings);
                var clientNames = GetCustomClientName(appSettings);
                var line = new CsvLine(clientNames, products);
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
            }
            catch (ConfigurationErrorsException e)
            {
                Logger.Error($"Error reading app settings: {e.StackTrace}");
            }

            return null;
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

        private static List<string> GetCustomClientName(NameValueCollection appSettings)
        {
            return appSettings["clientNames"]?.Split(',').ToList();
        }

        private static List<Product> GetCustomProduct(NameValueCollection appSettings)
        {
            var productNames = appSettings["products"]?.Split(',');
            if (productNames == null)
            {
                return null;
            }

            var list = new List<Product>(0);
            list.AddRange(productNames.Select(name => new Product(name)));
            return list;
        }
    }
}