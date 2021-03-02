using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Task5.BL.Contacts;
using Task5.BL.Enums;
using Task5EpamCourse.Models.Product;
using Task5EpamCourse.PageHelper.Contacts;
using Task5EpamCourse.Service.Contracts;

namespace Task5EpamCourse.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPageService _pageService;
        private readonly IProductMapper _productMapper;

        public ProductController(IProductService productService, IPageService pageService, IProductMapper productMapper)
        {
            _productService = productService;
            _pageService = pageService;
            _productMapper = productMapper;
        }


        [Authorize]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return View("Error");
            }

            return View(_pageService.GetModelsInPageViewModel<ProductViewModel>(page));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string fieldString, TextFieldFilter filter, int page = 1)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (filter == TextFieldFilter.Price)
                {
                    try
                    {
                        Double.Parse(fieldString);
                        return View(
                            _pageService.GetFilteredModelsInPageViewModel<ProductViewModel>(filter, fieldString, page));
                    }
                    catch (Exception e)
                    {
                        ViewBag.NotValidParse = "Неверный ввод цены, примерный ввод : 0,3";
                        return View(_pageService.GetModelsInPageViewModel<ProductViewModel>(page));
                    }
                }
                else
                {
                    return View(
                    _pageService.GetFilteredModelsInPageViewModel<ProductViewModel>(filter, fieldString, page));
                }
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
                return View(_productMapper.GetProductViewModel(id));
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
        public ActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                productViewModel.Id = _productService.GetProductsDto().ToList().Count;
                _productService.AddProduct(_productMapper.GetProductDto(productViewModel));
                return View("Details", productViewModel);
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
                return View(_productMapper.GetProductViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _productService.ModifyProduct(_productMapper.GetProductDto(productViewModel));
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
                return View(_productMapper.GetProductViewModel(id));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(ProductViewModel productViewModel)
        {
            if (productViewModel.Id != 0)
            {
                _productService.RemoveProduct(_productMapper.GetProductDto(productViewModel));
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}