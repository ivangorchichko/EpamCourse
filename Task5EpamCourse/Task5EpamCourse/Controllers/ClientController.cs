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
        private readonly IClientService _clientService;
        private readonly ILogger _logger;

        public ClientController(IClientService clientService, ILogger logger)
        {
            _clientService = clientService;
            _logger = logger;
        }


        [Authorize]
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            _logger.Debug("Running Index Get method in ClientController");
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                _logger.Error("Error with authenticated");
                return View("Error");
            }
            _logger.Debug("Sharing Index view");
            return View(PageService.GetClientsPages(
                MapperWebService.GetClientViewModels(_clientService.GetClientDto()), page));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string fieldString, TextFieldFilter filter, int page = 1)
        {
            _logger.Debug("Running Index Post method in ClientController");
            if (HttpContext.User.Identity.IsAuthenticated == false)
            {
                _logger.Error("Error with authenticated");
                return View("Error");
            }
            _logger.Debug("Sharing Index view");
            var clients = MapperWebService
                .GetClientViewModels(_clientService.GetFilteredClientDto(filter, fieldString));
            return View(PageService.GetClientsPages(clients, page));
        }

        [Authorize]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            _logger.Debug("Running Details Get method in ClientController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Details view");
                var client = MapperWebService.GetClientViewModels(_clientService.GetClientDto())
                    .ToList().Find(x => x.Id == id);
                return View(client);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            _logger.Debug("Running Create Get method in ClientController, sharing Create view");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(ClientViewModel clientViewModel)
        {
            _logger.Debug("Running Create Post method in ClientController");
            if (ModelState.IsValid)
            {
                _logger.Debug("Adding new client");
                clientViewModel.Id = _clientService.GetClientDto().ToList().Count;
                _clientService.AddClient(MapperWebService.GetClientDto(clientViewModel));
                _logger.Debug("Adding complete");
                return View("Details", clientViewModel);
            }
            else
            {
                _logger.Error("Error in model state");
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Modify(int? id)
        {
            _logger.Debug("Running Modify Get method in ClientController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Modify view");
                var client = MapperWebService.GetClientViewModels(_clientService.GetClientDto())
                    .ToList().Find(x => x.Id == id);
                return View(client);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Modify(ClientViewModel clientViewModel)
        {
            _logger.Debug("Running Modify Post method in ClientController");
            if (ModelState.IsValid)
            {
                _logger.Debug("Modify client model");
                _clientService.ModifyClient(MapperWebService.GetClientDto(clientViewModel));
                _logger.Debug("Modify complete");
                return RedirectToAction("Index");
            }
            else
            {
                _logger.Error("Error in model state");
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            _logger.Debug("Running Delete Get method in ClientController");
            if (id == null)
            {
                _logger.Error("Error in chosen model");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.Debug("Sharing Delete view");
                var client = MapperWebService.GetClientViewModels(_clientService.GetClientDto())
                    .ToList().Find(x => x.Id == id);
                return View(client);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(ClientViewModel clientViewModel)
        {
            _logger.Debug("Running Delete Post method in ClientController");
            if (ModelState.IsValid)
            {
                _logger.Debug("Remove client from db");
                _clientService.RemoveClient(MapperWebService.GetClientDto(clientViewModel));
                _logger.Debug("Remove complete");
                return RedirectToAction("Index");
            }
            else
            {
                _logger.Error("Error in chosen model");
                return View();
            }
        }
    }
}