using System.Linq;
using System.Net;
using System.Web.Mvc;
using Task5.BL.Enums;
using Task5.BL.Service;
using Task5.DAL.Repository.Contract;
using Task5.DAL.Repository.Model;
using Task5EpamCourse.MapperWebHelper;
using Task5EpamCourse.Models.Purchase;
using Task5EpamCourse.PageHelper;

namespace Task5EpamCourse.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        private static readonly IRepository Repository = new Repository();
        private static readonly PurchaseService PurchaseService = new PurchaseService(Repository);

        [Authorize]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return View("Error");
            }

            return View(PagingHelper.GetPurchasesPages(
                MapperWebService.GetPurchasesViewModels(PurchaseService.GetPurchaseDto()), page));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string fieldString, TextFieldFilter filter, int page = 1)
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return View("Error");
            }
            var purchases = MapperWebService
                .GetPurchasesViewModels(PurchaseService.GetFilteresPurchaseDto(filter,fieldString));
            return View(PagingHelper.GetPurchasesPages(purchases, page));
        }

        [Authorize]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var purchase = MapperWebService.GetPurchasesViewModels(PurchaseService.GetPurchaseDto())
                    .ToList().Find(x => x.Id == id);
                return View(MapperWebService.GetDetailsPurchaseViewModel(purchase));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(CreatePurchaseViewModel purchaseViewModel)
        {
            if (ModelState.IsValid)
            {
                //???
                purchaseViewModel.Id = PurchaseService.GetPurchaseDto().ToList().Count;
                PurchaseService.AddPurchase(MapperWebService.GetPurchaseDto(purchaseViewModel));
                return View("Details", MapperWebService.GetDetailsPurchaseViewModel(purchaseViewModel));
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Modify(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var purchase = MapperWebService.GetPurchasesViewModels(PurchaseService.GetPurchaseDto())
                    .ToList().Find(x => x.Id == id);
                return View(MapperWebService.GetModifyPurchaseViewModel(purchase));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(ModifyPurchaseViewModel purchaseViewModel)
        {
            if (ModelState.IsValid)
            {
                PurchaseService.ModifyPurchase(MapperWebService.GetPurchaseDto(purchaseViewModel));
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var purchase = MapperWebService.GetPurchasesViewModels(PurchaseService.GetPurchaseDto())
                    .ToList().Find(x => x.Id == id);
                return View(MapperWebService.GetDeletePurchaseViewModel(purchase));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(DeletePurchaseViewModel purchaseViewModel)
        {
            if (ModelState.IsValid)
            {
                PurchaseService.RemovePurchase(MapperWebService.GetPurchaseDto(purchaseViewModel));
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
