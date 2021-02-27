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
using Task5EpamCourse.Models.Purchase;
using Task5EpamCourse.PageHelper;

namespace Task5EpamCourse.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        // GET: Client
        private static readonly IRepository Repository = new Repository();
        private static readonly ClientService ClientService = new ClientService(Repository);

        [Authorize]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return View("Error");
            }

            return View(PagingHelper.GetClientsPages(
                MapperWebService.GetClientViewModels(ClientService.GetClientDto()), page));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string fieldString, TextFieldFilter filter, int page = 1)
        {
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                return View("Error");
            }
            var clients = MapperWebService
                .GetClientViewModels(ClientService.GetFilteredClientDto(filter, fieldString));
            return View(PagingHelper.GetClientsPages(clients, page));
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
                var client = MapperWebService.GetClientViewModels(ClientService.GetClientDto())
                    .ToList().Find(x => x.Id == id);
                return View(MapperWebService.GetDetailsClientViewModel(client));
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
        public ActionResult Create(CreateClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                //???
                clientViewModel.Id = ClientService.GetClientDto().ToList().Count;
                ClientService.AddClient(MapperWebService.GetClientDto(clientViewModel));
                return View("Details", MapperWebService.GetDetailsClientViewModel(clientViewModel));
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
                var client = MapperWebService.GetClientViewModels(ClientService.GetClientDto())
                    .ToList().Find(x => x.Id == id);
                return View(MapperWebService.GetModifyClientViewModel(client));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(ModifyClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                ClientService.ModifyClient(MapperWebService.GetClientDto(clientViewModel));
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
                var client = MapperWebService.GetClientViewModels(ClientService.GetClientDto())
                    .ToList().Find(x => x.Id == id);
                return View(MapperWebService.GetDeleteClientViewModel(client));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(DeleteClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                ClientService.RemoveClient(MapperWebService.GetClientDto(clientViewModel));
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}