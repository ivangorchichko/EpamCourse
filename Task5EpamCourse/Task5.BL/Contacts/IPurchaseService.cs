using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.BL.Enums;
using Task5.BL.Models;

namespace Task5.BL.Contacts
{
    public interface IPurchaseService
    {
        IEnumerable<PurchaseDto> GetPurchaseDto();

        void AddPurchase(PurchaseDto purchaseDto);

        void ModifyPurchase(PurchaseDto purchaseDto);

        void RemovePurchase(PurchaseDto purchaseDto);

        IEnumerable<PurchaseDto> GetFilteredPurchaseDto(TextFieldFilter filter, string fieldString);
    }
}
