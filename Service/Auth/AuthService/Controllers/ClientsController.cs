using AuthService.Common;
using AuthService.Common.Interfaces.Services;
using AuthService.Helpers;
using AuthService.ViewModels;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = Const.DefaultRoles.Admin)]
    public class ClientsController : Controller
    {
        private readonly IClientService _service;
        private readonly IClientStore _clients;

        public ClientsController(IClientService service, IClientStore clients)
        {
            _service = service;
            _clients = clients;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new ClientsViewModel { Clients = await _service.GetListClientsAsync() };
            var errMsg = TempData["ErrorMessage"] as string;
            if (!string.IsNullOrWhiteSpace(errMsg))
                ModelState.AddModelError("", errMsg);
            return View(vm);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New([FromBody] Client client)
        {
            var success = await _service.AddClientAsync(client);
            if (success)
                return Success();
            return Error();
        }

        private IActionResult Success()
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Content("Success!", MediaTypeNames.Text.Plain);
        }
        private IActionResult Error()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Content("Error!", MediaTypeNames.Text.Plain);
        }
    }
}
