using AutoMapper;
using Task5.BL.Models;
using Task5EpamCourse.Models.Client;
using Task5EpamCourse.Models.Product;
using Task5EpamCourse.Models.Purchase;

namespace Task5EpamCourse.MapperWebHelper
{
    public static class AutoMapperWebConfig
    {
        public static MapperConfiguration Configure()
        {
            return new MapperConfiguration(conf =>
            {
                //Purchase//
                conf.CreateMap<PurchaseDto, PurchaseViewModel>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.Client.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.Client.ClientTelephone))
                    .ForPath(x => x.ProductName, y => y.MapFrom(z => z.Product.ProductName))
                    .ForPath(x => x.Price, y => y.MapFrom(z => z.Product.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<PurchaseViewModel, PurchaseDto>()
                    .ForPath(x => x.Client.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.Client.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Product.ProductName, y => y.MapFrom(z => z.ProductName))
                    .ForPath(x => x.Product.Price, y => y.MapFrom(z => z.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));

                //Client//
                conf.CreateMap<ClientDto, ClientViewModel>();
                conf.CreateMap<ClientViewModel, ClientDto>();
                   
                //Product//
                conf.CreateMap<ProductDto, ProductViewModel>();
                conf.CreateMap<ProductViewModel, ProductDto>();
                
            });
        }
    }
}
