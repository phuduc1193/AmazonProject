using AuthService.Common;
using AuthService.Common.Interfaces.Services;
using AuthService.Helpers;
using AuthService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = Const.DefaultRoles.Admin)]
    public class ClientsController : Controller
    {
        private readonly IClientService _service;

        public ClientsController(IClientService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new ClientsViewModel { Clients = await _service.GetListClientsAsync() };
            var errMsg = TempData["ErrorMessage"] as string;
            if (!string.IsNullOrWhiteSpace(errMsg))
                ModelState.AddModelError("", errMsg);
            return View(vm);
        }
    }
}
