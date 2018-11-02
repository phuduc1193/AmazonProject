using AuthService.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = "Administrator")]
    public class ApiResourcesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}