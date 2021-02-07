using Autofac;
using Serilog;
using Task4.BL.Contracts;
using Task4.BL.CSVService;
using Task4.BL.Enum;
using Task4.BL.Logger;
using Task4.BL.Service;

namespace Task4.BL.DependenciesConfig
{
    public static class AutofacConfiguration 
    {
        public static IContainer ConfigureContainer(LoggerType type)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CatalogHandler>().As<ICatalogHandler>();
            builder.RegisterType<CatalogWatcher>().As<ICatalogWatcher>();
            builder.RegisterType<CsvParser>().As<ICsvParser>();
            builder.RegisterType<ServerOperations>().As<IServerOperations>();
            builder.RegisterType<TaskManager>().As<ITaskManager>();
            switch (type)
            {
                case LoggerType.Console:
                {
                    builder.RegisterInstance<ILogger>(LoggerFactory.GetLogger(type));
                    break;
                }
                case LoggerType.File:
                {
                    builder.RegisterInstance<ILogger>(LoggerFactory.GetLogger(type));
                    break;
                }
            }
            return builder.Build();
        }
    }
}
