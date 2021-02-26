using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Task5.BL.Models;
using Task5.DomainModel.DataModel;

namespace Task5.BL.MapperBLHelper
{
    public static class AutoMapperBLConfig
    {
        public static MapperConfiguration Configure()
        {
            return new MapperConfiguration(conf =>
            {
                conf.CreateMap<ClientEntity, ClientDto>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<ProductEntity, ProductDto>()
                    .ForPath(x => x.ProductName, y => y.MapFrom(z => z.ProductName))
                    .ForPath(x => x.Price, y => y.MapFrom(z => z.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<PurchaseEntity, PurchaseDto>()
                    .ForPath(x => x.Client.ClientName, o => o.MapFrom(z => z.Client.ClientName))
                    .ForPath(x => x.Client.ClientTelephone, y => y.MapFrom(z => z.Client.ClientTelephone))
                    .ForPath(x=>x.Client.Id, y=>y.MapFrom(z=>z.Client.Id))
                    .ForPath(x => x.Product.ProductName, y => y.MapFrom(z => z.Product.ProductName))
                    .ForPath(x => x.Product.Price, y => y.MapFrom(z => z.Product.Price))
                    .ForPath(x => x.Product.Id, y => y.MapFrom(z => z.Product.Id))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
                conf.CreateMap<PurchaseDto, PurchaseEntity>()
                    .ForPath(x => x.Client.ClientName, o => o.MapFrom(z => z.Client.ClientName))
                    .ForPath(x => x.Client.ClientTelephone, y => y.MapFrom(z => z.Client.ClientTelephone))
                    .ForPath(x => x.Product.ProductName, y => y.MapFrom(z => z.Product.ProductName))
                    .ForPath(x => x.Product.Price, y => y.MapFrom(z => z.Product.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id));
            });
        }
    }
}
