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
using Serilog;
using Task4.BL.Contracts;
using Task4.BL.Service;

namespace Task4.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private static ILogger Logger;

        private static ICatalogWatcher Watcher;
        private static ITaskManager Manager;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Watcher
                = new CatalogWatcher(new FileSystemWatcher(ConfigurationManager.AppSettings.Get("receivedFolder")), Logger);
            Manager = new TaskManager(3, Watcher, Logger);
            Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                //.WriteTo.File(ConfigurationManager.AppSettings.Get("loggerFile"),
                //    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            Logger.Verbose("Application started");
            Watcher.Start();
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            Watcher.Stop();
            Logger.Verbose("Application stopped");
            Watcher.Dispose();
            Manager.Dispose();
            base.OnStop();
        }
    }

}
