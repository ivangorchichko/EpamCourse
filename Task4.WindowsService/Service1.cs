using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Task4.BL.Contracts;
using Task4.BL.Service;

namespace Task4.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private static readonly ICatalogWatcher Watcher
            = new CatalogWatcher(new FileSystemWatcher(ConfigurationManager.AppSettings.Get("sourceFolder")));

        private static readonly ITaskManager Manager
            = new TaskManager(new CustomTaskScheduler(3), Watcher);
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Watcher.Start();
        }

        protected override void OnStop()
        {
            Watcher.Stop();
            Watcher.Dispose(true);
            Manager.Dispose(true);
        }
    }
}
