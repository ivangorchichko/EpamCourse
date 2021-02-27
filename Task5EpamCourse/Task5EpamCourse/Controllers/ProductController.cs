using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Task5.BL.Enums;
using Task5.BL.Service;
using Task5.DAL.Repository.Contract;
using Task5.DAL.Repository.Model;
using Task5EpamCourse.MapperWebHelper;
using Task5EpamCourse.Models.Client;
using Task5EpamCourse.Models.Product;
using Task5EpamCourse.PageHelper;

namespace Task5EpamCourse.Controllers
{
    public class ProductController : Controller
    {
        private static readonly IRepository Repository = new Repository();
        private static readonly ProductService ProductService = new ProductService(Repository);

        [Authorize]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return View("Error");
            }

            return View(PagingHelper.GetProductPages(
                MapperWebService.GetProductViewModels(ProductService.GetProductDto()), page));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string fieldString, TextFieldFilter filter, int page = 1)
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return View("Error");
            }
            var products = MapperWebService
                .GetProductViewModels(ProductService.GetFilteredProductDto(filter, fieldString));
            return View(PagingHelper.GetProductPages(products, page));
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
                var product = MapperWebService.GetProductViewModels(ProductService.GetProductDto())
                    .ToList().Find(x => x.Id == id);
                return View(MapperWebService.GetDetailsProductViewModel(product));
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
        public ActionResult Create(CreateProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                //???
                productViewModel.Id = ProductService.GetProductDto().ToList().Count;
                ProductService.AddProduct(MapperWebService.GetProductDto(productViewModel));
                return View("Details", MapperWebService.GetDetailsProductViewModel(productViewModel));
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
                var product = MapperWebService.GetProductViewModels(ProductService.GetProductDto())
                    .ToList().Find(x => x.Id == id);
                return View(MapperWebService.GetModifyProductViewModel(product));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(ModifyProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductService.ModifyProduct(MapperWebService.GetProductDto(productViewModel));
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
                var product = MapperWebService.GetProductViewModels(ProductService.GetProductDto())
                    .ToList().Find(x => x.Id == id);
                return View(MapperWebService.GetDeleteProductViewModel(product));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(DeleteProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductService.RemoveProduct(MapperWebService.GetProductDto(productViewModel));
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}