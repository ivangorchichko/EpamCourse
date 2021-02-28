using System.Linq;
using System.Net;
using System.Web.Mvc;
using Task5.BL.Contacts;
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
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error");
            }

            return View(PagingHelper.GetPurchasesPages(
                MapperWebService.GetPurchasesViewModels(_purchaseService.GetPurchaseDto()), page));
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
                .GetPurchasesViewModels(_purchaseService.GetFilteredPurchaseDto(filter,fieldString));
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
                var purchase = MapperWebService.GetPurchasesViewModels(_purchaseService.GetPurchaseDto())
                    .ToList().Find(x => x.Id == id);
                return View(purchase);
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
        public ActionResult Create(PurchaseViewModel purchaseViewModel)
        {
            if (ModelState.IsValid)
            {
                //???
                purchaseViewModel.Id = _purchaseService.GetPurchaseDto().ToList().Count;
                _purchaseService.AddPurchase(MapperWebService.GetPurchaseDto(purchaseViewModel));
                return View("Details",purchaseViewModel);
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
                var purchase = MapperWebService.GetPurchasesViewModels(_purchaseService.GetPurchaseDto())
                    .ToList().Find(x => x.Id == id);
                return View(purchase);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(PurchaseViewModel purchaseViewModel)
        {
            if (ModelState.IsValid)
            {
                _purchaseService.ModifyPurchase(MapperWebService.GetPurchaseDto(purchaseViewModel));
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
                var purchase = MapperWebService.GetPurchasesViewModels(_purchaseService.GetPurchaseDto())
                    .ToList().Find(x => x.Id == id);
                return View(purchase);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(PurchaseViewModel purchaseViewModel)
        {
            if (ModelState.IsValid)
            {
                _purchaseService.RemovePurchase(MapperWebService.GetPurchaseDto(purchaseViewModel));
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
