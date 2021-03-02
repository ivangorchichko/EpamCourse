using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.BL.Models;
using Task5EpamCourse.Models.Manager;
using Task5EpamCourse.Models.Purchase;

namespace Task5EpamCourse.Service.Contracts
{
    public interface IPurchaseMapper
    {
        PurchaseViewModel GetPurchaseViewModel(int? id);

        PurchaseDto GetPurchaseDto(PurchaseViewModel purchaseViewModel);

        PurchaseDto GetPurchaseDto(CreatePurchaseViewModel createPurchaseViewModel);

        PurchaseDto GetPurchaseDto(ModifyPurchaseViewModel modifyPurchaseViewModel);

        IEnumerable<PurchaseViewModel> GetPurchaseViewModel();

        PurchaseViewModel GetPurchaseViewModel(CreatePurchaseViewModel createPurchase);

        CreatePurchaseViewModel GetManagersViewModel();

        ModifyPurchaseViewModel GetModifyPurchaseViewModel(int? id);
    }
}
