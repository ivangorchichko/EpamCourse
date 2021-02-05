using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XmlConfiguration;
using Autofac;
using Autofac.Core;
using Task4.BL.Contracts;
using Task4.BL.CSVService;
using Task4.BL.Service;

namespace Task4.BL.DependenciesConfig
{
    public class AutofucConfig 
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CatalogHandler>().As<ICatalogHandler>();
            builder.RegisterType<CatalogWatcher>().As<ICatalogWatcher>();
            builder.RegisterType<CsvParser>().As<ICsvParser>();
            builder.RegisterType<ServerOperations>().As<IServerOperations>();
            builder.RegisterType<TaskManager>().As<ITaskManager>();

            return builder.Build();
        }
    }
}
