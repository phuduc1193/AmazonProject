using AuthService.Common;
using AuthService.Common.Interfaces.Services;
using AuthService.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = Const.DefaultRoles.Admin)]
    public class ClaimsController : Controller
    {
        private readonly IClaimService _service;

        public ClaimsController(IClaimService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
