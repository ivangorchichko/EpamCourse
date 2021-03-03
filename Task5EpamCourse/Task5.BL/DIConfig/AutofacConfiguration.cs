using System.Runtime.CompilerServices;
using Autofac;
using Task5.BL.Contacts;
using Task5.BL.Logger;
using Task5.BL.Service;
using Task5.DAL.Repository.Contract;
using Task5.DAL.Repository.Model;
using IContainer = System.ComponentModel.IContainer;

namespace Task5.BL.DIConfig
{
    public static class AutofacConfiguration
    {

        public static ContainerBuilder ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ClientService>().As<IClientService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<PurchaseService>().As<IPurchaseService>();
            builder.RegisterType<Repository>().As<IRepository>();
            builder.RegisterType<ManagerService>().As<IManagerService>();


            return builder;
        }
    }
}
