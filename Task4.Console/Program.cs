using System;
using System.Configuration;
using System.Threading;
using NLog;

namespace Task4.Console
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static void Main(string[] args)
        {
            try
            {
                var watcher = new CsvFileWatcher(GetWatchingDir());
                var thread = new Thread(watcher.StartWatch);
                thread.Start();
                
                Thread.Sleep(60000);
                
                watcher.StopWatch();
                Thread.Sleep(1000);
            }
            catch (ArgumentNullException e)
            {
                Logger.Error($"{e.Message}: {e.StackTrace}");
            }
            catch (ArgumentException e)
            {
                Logger.Error($"The path <{e.ParamName}> doesn't exist");
            }
        }

        private static string GetWatchingDir()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings.Count > 0)
                {
                    return appSettings["watchingDir"] ?? "";
                }

                Logger.Warn("App settings is empty");
            }
            catch (ConfigurationErrorsException e)
            {
                Logger.Error($"Error reading app settings: {e.StackTrace}");    
            }
            
            return null;
        }
    }
}