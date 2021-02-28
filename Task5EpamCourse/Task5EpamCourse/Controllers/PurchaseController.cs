using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Task5.BL.Contacts;
using Task5.BL.Enums;
using Task5.BL.Service;
using Task5.DAL.Repository.Contract;
using Task5.DAL.Repository.Model;
using Task5EpamCourse.MapperWebHelper;
using Task5EpamCourse.Models.Manager;
using Task5EpamCourse.Models.Purchase;
using Task5EpamCourse.PageHelper;
using Task5EpamCourse.PageHelper.Contacts;
using Task5EpamCourse.Service;
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

            return View(_pageService.GetModelsInPageViewModel<PurchaseViewModel>(page));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string fieldString, TextFieldFilter filter, int page = 1)
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return View("Error");
            }
            
            return View(_pageService.GetFilteredModelsInPageViewModel<PurchaseViewModel>(filter, fieldString, page));
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
                var purchase = _purchaseMapper.GetPurchaseViewModel(id);
                return View(purchase);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            List<string> managerNames = new List<string>();
            foreach (var purchase in _purchaseMapper.GetPurchaseViewModel())
            {
                if (!managerNames.Contains(purchase.ManagerName))
                {
                    managerNames.Add(purchase.ManagerName);
                }
            }

            ViewBag.Managers = managerNames;
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(PurchaseViewModel purchaseViewModel, string selectManager)
        {
            if (ModelState.IsValid)
            {
                _purchaseService.AddPurchase(_purchaseMapper.GetPurchaseDto(purchaseViewModel), selectManager);
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
                var purchase = _purchaseMapper.GetPurchaseViewModel(id);
                return View(purchase);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(PurchaseViewModel purchaseViewModel)
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
                var purchase = _purchaseMapper.GetPurchaseViewModel(id);
                return View(purchase);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(PurchaseViewModel purchaseViewModel)
        {
            if (ModelState.IsValid)
            {
                _purchaseService.RemovePurchase(_purchaseMapper.GetPurchaseDto(purchaseViewModel));
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
