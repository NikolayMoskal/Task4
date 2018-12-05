using System;
using System.Collections.Generic;
using NLog;
using Task4.Generator.Models;

namespace Task4
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static void Main(string[] args)
        {
            var fileName = new SurnameDateFileName("Moskal", DateTime.Today, "_");
            try
            {
                var products = new List<Product>
                {
                    new Product("ABC", 12.5),
                    new Product("DEF", 11.7),
                    new Product("GHI", 10.4)
                };
                var line = new CsvLine("Moskal", products);
                new CsvGenerator(fileName, line).Generate(@"C:\", 50);
            }
            catch (ArgumentException e)
            {
                Logger.Error($"Wrong argument: {e.Message}. Stack trace: {e.StackTrace}");
            }
        }
    }
}