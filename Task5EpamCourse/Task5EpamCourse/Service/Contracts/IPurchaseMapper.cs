using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.BL.Models;
using Task5EpamCourse.Models.Purchase;

namespace Task5EpamCourse.Service.Contracts
{
    public interface IPurchaseMapper
    {
        PurchaseViewModel GetPurchaseViewModel(int? id);

        PurchaseDto GetPurchaseDto(PurchaseViewModel purchaseViewModel);

        IEnumerable<PurchaseViewModel> GetPurchaseViewModel();
    }
}
