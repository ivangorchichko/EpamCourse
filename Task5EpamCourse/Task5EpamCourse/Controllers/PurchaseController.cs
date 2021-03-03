using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Task5.BL.Contacts;
using Task5.BL.Enums;
using Task5EpamCourse.Models.Purchase;
using Task5EpamCourse.PageHelper.Contacts;
using Task5EpamCourse.Service.Contracts;

namespace Task5EpamCourse.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IPageService _pageService;
        private readonly IPurchaseMapper _purchaseMapper;

        public PurchaseController(IPurchaseService purchaseService, IPageService pageService, IPurchaseMapper purchaseMapper)
        {
            _purchaseService = purchaseService;
            _pageService = pageService;
            _purchaseMapper = purchaseMapper;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error");
            }
            ViewBag.CurrentPage = page;
            return View();
        }

        public ActionResult GetPurchases(int page = 1)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error");
            }
            ViewBag.CurrentPage = page;
            return PartialView("_Purchases",_pageService.GetModelsInPageViewModel<PurchaseViewModel>(page));
        }

        [HttpPost]
        public ActionResult GetPurchases(string fieldString, TextFieldFilter filter, int page = 1)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (filter == TextFieldFilter.Date)
                {
                    try
                    {
                        DateTime.Parse(fieldString);
                        return PartialView("_Purchases",
                            _pageService.GetFilteredModelsInPageViewModel<PurchaseViewModel>(filter, fieldString,
                                page));
                    }
                    catch (Exception e)
                    {
                        ViewBag.NotValidParse = "Неверный ввод даты, примерный ввод : 11.11.2011";
                        return PartialView("_Purchases",_pageService.GetModelsInPageViewModel<PurchaseViewModel>(page));
                    }
                }
                return PartialView("_Purchases",
                    _pageService.GetFilteredModelsInPageViewModel<PurchaseViewModel>(filter, fieldString,
                        page));
            }
            return View("Error");
            
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
                return View(_purchaseMapper.GetPurchaseViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View(_purchaseMapper.GetManagersViewModel());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(CreatePurchaseViewModel purchaseViewModel)
        {
            if (ModelState.IsValid)
            {
                _purchaseService.AddPurchase(_purchaseMapper.GetPurchaseDto(purchaseViewModel));
                return View("Details", _purchaseMapper.GetPurchaseViewModel(purchaseViewModel));
            }
            else
            {
                return View(_purchaseMapper.GetManagersViewModel());
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
                return View(_purchaseMapper.GetModifyPurchaseViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(ModifyPurchaseViewModel purchaseViewModel)
        {
            if (ModelState.IsValid)
            {
                _purchaseService.ModifyPurchase(_purchaseMapper.GetPurchaseDto(purchaseViewModel));
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
                return View(_purchaseMapper.GetPurchaseViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(PurchaseViewModel purchaseViewModel)
        {
            if (purchaseViewModel.Id != 0)
            {
                _purchaseService.RemovePurchase(_purchaseMapper.GetPurchaseDto(_purchaseMapper.GetPurchaseViewModel(purchaseViewModel.Id)));
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public JsonResult GetChartData()
        {
            var item = _purchaseService.GetPurchaseDto()
                .GroupBy(x => x.Date.ToString("d"))
                .Select(x => new object[] { x.Key.ToString(), x.Count() })
                .ToArray();
            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}
