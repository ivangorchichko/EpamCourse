using Autofac;
using Task5.BL.Contacts;
using Task5.BL.Logger;
using Task5.BL.Service;

namespace Task5.BL.DIConfig
{
    public static class AutofacConfiguration
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ClientService>().As<IClientService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<PurchaseService>().As<IPurchaseService>();
            builder.RegisterInstance(LoggerFactory.GetLogger());
            
            return builder.Build();
        }
    }
}
