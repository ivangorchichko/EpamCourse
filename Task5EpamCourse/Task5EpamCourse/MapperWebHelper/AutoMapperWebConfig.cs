using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Task5.BL.Models;
using Task5.DomainModel.DataModel;
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
                conf.CreateMap<PurchaseDto, IndexPurchaseViewModel>();
                conf.CreateMap<CreatePurchaseViewModel, DetailsPurchaseViewModel>();
                conf.CreateMap<IndexPurchaseViewModel, DeletePurchaseViewModel>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.Client.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.Client.ClientTelephone))
                    .ForPath(x => x.ProductName, y => y.MapFrom(z => z.Product.ProductName))
                    .ForPath(x => x.Price, y => y.MapFrom(z => z.Product.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<IndexPurchaseViewModel, DetailsPurchaseViewModel>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.Client.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.Client.ClientTelephone))
                    .ForPath(x => x.ProductName, y => y.MapFrom(z => z.Product.ProductName))
                    .ForPath(x => x.Price, y => y.MapFrom(z => z.Product.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<CreatePurchaseViewModel, PurchaseDto>()
                    .ForPath(x => x.Client.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.Client.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Product.ProductName, y => y.MapFrom(z => z.ProductName))
                    .ForPath(x => x.Product.Price, y => y.MapFrom(z => z.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<DeletePurchaseViewModel, PurchaseDto>()
                    .ForPath(x => x.Client.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.Client.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Product.ProductName, y => y.MapFrom(z => z.ProductName))
                    .ForPath(x => x.Product.Price, y => y.MapFrom(z => z.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<IndexPurchaseViewModel, PurchaseDto>()
                    .ForPath(x => x.Client.ClientName, o => o.MapFrom(z => z.Client.ClientName))
                    .ForPath(x => x.Client.ClientTelephone, y => y.MapFrom(z => z.Client.ClientTelephone))
                    .ForPath(x => x.Product.ProductName, y => y.MapFrom(z => z.Product.ProductName))
                    .ForPath(x => x.Product.Price, y => y.MapFrom(z => z.Product.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<IndexPurchaseViewModel, ModifyPurchaseViewModel>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.Client.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.Client.ClientTelephone))
                    .ForPath(x => x.ProductName, y => y.MapFrom(z => z.Product.ProductName))
                    .ForPath(x => x.Price, y => y.MapFrom(z => z.Product.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<ModifyPurchaseViewModel, PurchaseDto>()
                    .ForPath(x => x.Client.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.Client.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Product.ProductName, y => y.MapFrom(z => z.ProductName))
                    .ForPath(x => x.Product.Price, y => y.MapFrom(z => z.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));

                //Client//
                conf.CreateMap<ClientDto, IndexClientViewModel>();
                conf.CreateMap<CreateClientViewModel, DetailsClientViewModel>();
                conf.CreateMap<IndexClientViewModel, DeleteClientViewModel>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<IndexClientViewModel, DetailsClientViewModel>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<CreateClientViewModel, ClientDto>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<DeleteClientViewModel, ClientDto>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<IndexClientViewModel, ClientDto>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<IndexClientViewModel, ModifyClientViewModel>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<ModifyClientViewModel, ClientDto>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));

                //Product//
                conf.CreateMap<ProductDto, IndexProductViewModel>();
                conf.CreateMap<CreateProductViewModel, DetailsProductViewModel>();
                conf.CreateMap<IndexProductViewModel, DeleteProductViewModel>();
                conf.CreateMap<IndexProductViewModel, DetailsProductViewModel>();
                conf.CreateMap<CreateProductViewModel, ProductDto>();
                conf.CreateMap<DeleteProductViewModel, ProductDto>();
                conf.CreateMap<IndexProductViewModel, ProductDto>();
                conf.CreateMap<IndexProductViewModel, ModifyProductViewModel>();
                conf.CreateMap<ModifyProductViewModel, ProductDto>();
            });
        }
    }
}
