using System.Configuration;
using System.IO;
using System.ServiceProcess;
using Serilog;
using Task4.BL.Contracts;
using Task4.BL.CSVService;
using Task4.BL.Enum;
using Task4.BL.Logger;
using Task4.BL.Service;

namespace Task4.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private static ILogger _logger;
        private static ICsvParser _parser;
        private static ICatalogWatcher _watcher;
        private static ITaskManager _manager;
        public Service1()
        {
            _logger = LoggerFactory.GetLogger(LoggerType.File);
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _parser = new CsvParser();
            _watcher
                = new CatalogWatcher(new FileSystemWatcher(ConfigurationManager.AppSettings.Get("receivedFolder")), _logger);
            _manager = new TaskManager( _watcher, _logger, _parser);
            _logger.Verbose("Application started");
            _watcher.Start();
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            _watcher.Stop();
            _logger.Verbose("Application stopped");
            _watcher.Dispose();
            _manager.Dispose();
            base.OnStop();
        }
    }

}
