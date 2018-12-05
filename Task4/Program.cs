using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Task4.Generator.Models;

namespace Task4
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static string _directoryPath;
        private static string _employeeName;
        private static int _linesCount;
        
        public static void Main(string[] args)
        {
            if (!GetAppSettings())
            {
                return;
            }
            
            var fileName = new SurnameDateFileName(_employeeName, DateTime.Today, "_");
            try
            {
                var products = new List<Product>
                {
                    new Product("ABC"),
                    new Product("DEF"),
                    new Product("GHI")
                };
                var line = new CsvLine(_employeeName, products);
                new CsvGenerator(fileName, line).Generate(_directoryPath, _linesCount);
            }
            catch (ArgumentException e)
            {
                Logger.Error($"Wrong argument: {e.Message}. Stack trace: {e.StackTrace}");
            }
        }

        private static bool GetAppSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count <= 0)
                {
                    Logger.Warn("App settings is empty");
                    return false;
                }

                _directoryPath = appSettings["directoryPath"];
                _employeeName = string.IsNullOrEmpty(appSettings["employeeName"]) 
                    ? "<empty>" 
                    : appSettings["employeeName"];
                int.TryParse(appSettings["linesCount"], out _linesCount);
                if (_linesCount == 0)
                {
                    Logger.Warn("Lines count in CSV file was set to 0. It is correct value?");
                }

                return true;
            }
            catch (ConfigurationErrorsException e)
            {
                Logger.Error($"Error reading app settings: {e.StackTrace}");
                return false;
            }
        }
    }
}