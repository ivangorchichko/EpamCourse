using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Task5EpamCourse.Models.Client;
using Task5EpamCourse.Models.Page;
using Task5EpamCourse.Models.Product;
using Task5EpamCourse.Models.Purchase;

namespace Task5EpamCourse.PageHelper
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }

        public static PurchasesInPageViewModel GetPurchasesPages(IEnumerable<IndexPurchaseViewModel> purchases, int page)
        {
            int pageSize = 3;
            IEnumerable<IndexPurchaseViewModel> purchasesPerPages = purchases.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = purchases.ToList().Count };
            return new PurchasesInPageViewModel { PageInfo = pageInfo, Purchases = purchasesPerPages };
        }

        public static ClientInPageViewModel GetClientsPages(IEnumerable<IndexClientViewModel> clients, int page)
        {
            int pageSize = 3;
            IEnumerable<IndexClientViewModel> clientsPerPages = clients.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = clients.ToList().Count };
            return new ClientInPageViewModel() { PageInfo = pageInfo, Clients = clientsPerPages };
        }

        public static ProductsInPageViewModel GetProductPages(IEnumerable<IndexProductViewModel> products, int page)
        {
            int pageSize = 3;
            IEnumerable<IndexProductViewModel> productsPerPages = products.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = products.ToList().Count };
            return new ProductsInPageViewModel() { PageInfo = pageInfo, Products = productsPerPages };
        }
    }
}