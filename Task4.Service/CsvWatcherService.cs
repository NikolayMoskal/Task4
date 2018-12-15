using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using BusinessLayer;
using NLog;

namespace Task4.Service
{
    public partial class CsvWatcherService : ServiceBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private CsvFileWatcher _watcher;
        
        public CsvWatcherService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _watcher = new CsvFileWatcher(GetWatchingDir());
            var thread = new Thread(_watcher.StartWatch);
            thread.Start();
        }

        protected override void OnStop()
        {
            _watcher.StopWatch();
            Thread.Sleep(1000);
        }
        
        private string GetWatchingDir()
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
