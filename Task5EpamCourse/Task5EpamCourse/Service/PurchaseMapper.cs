using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Task5.BL.Contacts;
using Task5.BL.Models;
using Task5EpamCourse.MapperWebHelper;
using Task5EpamCourse.Models.Purchase;
using Task5EpamCourse.Service.Contracts;

namespace Task5EpamCourse.Service
{
    public class PurchaseMapper : IPurchaseMapper
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseService _purchaseService;

        public PurchaseMapper(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
            _mapper = new Mapper(AutoMapperWebConfig.Configure());
        }

        public PurchaseViewModel GetPurchaseViewModel(int? id)
        {
            return _mapper.Map<IEnumerable<PurchaseViewModel>>(_purchaseService.GetPurchaseDto())
                    .ToList().Find(x => x.Id == id);
        }
        public IEnumerable<PurchaseViewModel> GetPurchaseViewModel()
        {
            return _mapper.Map<IEnumerable<PurchaseViewModel>>(_purchaseService.GetPurchaseDto())
                .OrderBy(d => d.Date);
        }
        public PurchaseDto GetPurchaseDto(PurchaseViewModel purchaseViewModel)
        {
            return _mapper.Map<PurchaseDto>(purchaseViewModel);
        }
    }
}