using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Serilog;
using Task5.BL.Contacts;
using Task5.BL.Enums;
using Task5.BL.Logger;
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
        private static readonly IClientService ClientService = new ClientService(Repository);
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        [Authorize]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            Logger.Debug("Running Index Get method in ClientController");
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                Logger.Error("Error with authenticated");
                return View("Error");
            }
            Logger.Debug("Sharing Index view");
            return View(PagingHelper.GetClientsPages(
                MapperWebService.GetClientViewModels(ClientService.GetClientDto()), page));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string fieldString, TextFieldFilter filter, int page = 1)
        {
            Logger.Debug("Running Index Post method in ClientController");
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                Logger.Error("Error with authenticated");
                return View("Error");
            }
            Logger.Debug("Sharing Index view");
            var clients = MapperWebService
                .GetClientViewModels(ClientService.GetFilteredClientDto(filter, fieldString));
            return View(PagingHelper.GetClientsPages(clients, page));
        }

        [Authorize]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            Logger.Debug("Running Details Get method in ClientController");
            if (id == null)
            {
                Logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Logger.Debug("Sharing Details view");
                var client = MapperWebService.GetClientViewModels(ClientService.GetClientDto())
                    .ToList().Find(x => x.Id == id);
                return View(MapperWebService.GetDetailsClientViewModel(client));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            Logger.Debug("Running Create Get method in ClientController, sharing Create view");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(CreateClientViewModel clientViewModel)
        {
            Logger.Debug("Running Create Post method in ClientController");
            if (ModelState.IsValid)
            {
                Logger.Debug("Adding new client");
                //???
                clientViewModel.Id = ClientService.GetClientDto().ToList().Count;
                ClientService.AddClient(MapperWebService.GetClientDto(clientViewModel));
                Logger.Debug("Adding complete");
                return View("Details", MapperWebService.GetDetailsClientViewModel(clientViewModel));
            }
            else
            {
                Logger.Error("Error in model state");
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Modify(int? id)
        {
            Logger.Debug("Running Modify Get method in ClientController");
            if (id == null)
            {
                Logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Logger.Debug("Sharing Modify view");
                var client = MapperWebService.GetClientViewModels(ClientService.GetClientDto())
                    .ToList().Find(x => x.Id == id);
                return View(MapperWebService.GetModifyClientViewModel(client));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(ModifyClientViewModel clientViewModel)
        {
            Logger.Debug("Running Modify Post method in ClientController");
            if (ModelState.IsValid)
            {
                Logger.Debug("Modify client model");
                ClientService.ModifyClient(MapperWebService.GetClientDto(clientViewModel));
                Logger.Debug("Modify complete");
                return RedirectToAction("Index");
            }
            else
            {
                Logger.Error("Error in model state");
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Logger.Debug("Running Delete Get method in ClientController");
            if (id == null)
            {
                Logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Logger.Debug("Sharing Delete view");
                var client = MapperWebService.GetClientViewModels(ClientService.GetClientDto())
                    .ToList().Find(x => x.Id == id);
                return View(MapperWebService.GetDeleteClientViewModel(client));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(DeleteClientViewModel clientViewModel)
        {
            Logger.Debug("Running Delete Post method in ClientController");
            if (ModelState.IsValid)
            {
                Logger.Debug("Remove client from db");
                ClientService.RemoveClient(MapperWebService.GetClientDto(clientViewModel));
                Logger.Debug("Remove complete");
                return RedirectToAction("Index");
            }
            else
            {
                Logger.Error("Error in chosen model");
                return View();
            }
        }
    }
}