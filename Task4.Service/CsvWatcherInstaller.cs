using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Task4.Service
{
    [RunInstaller(true)]
    public partial class CsvWatcherInstaller : Installer
    {
        public CsvWatcherInstaller()
        {
            InitializeComponent();
            Installers.Add(new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            });
            Installers.Add(new ServiceInstaller
            {
                StartType = ServiceStartMode.Manual,
                ServiceName = "CsvWatcherService"
            });
        }
    }
}
