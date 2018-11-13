using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using AuthService.Common;
using AuthService.Common.Interfaces.Services;
using AuthService.Helpers;
using AuthService.ViewModels;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthService.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = Const.DefaultRoles.Admin)]
    public class IdentityResourcesController : Controller
    {
        private IIdentityResourceService _service;

        public IdentityResourcesController(IIdentityResourceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new IdentityResourcesViewModel { IdentityResources = await _service.GetListIdentityResourcesAsync() };
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
        public async Task<IActionResult> New([FromBody] IdentityResource resource)
        {
            if (!IsValidResource(resource))
                return Error();

            var success = await _service.AddIdentityResourceAsync(resource);
            if (success)
                return Success();
            return Error();
        }

        private bool IsValidResource(IdentityResource resource)
        {
            if (resource == null
                || string.IsNullOrWhiteSpace(resource.Name)
                || string.IsNullOrWhiteSpace(resource.DisplayName))
                return false;
            if (resource.UserClaims == null || resource.UserClaims.Count == 0)
                return false;
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var jsonSerializerSettings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var resource = await _service.GetIdentityResourceByIdAsync(id);
            var jsonString = JsonConvert.SerializeObject(resource, Formatting.None, jsonSerializerSettings);
            ViewData["Data"] = jsonString;
            return View();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] IdentityResource resource)
        {
            if (!IsValidResource(resource))
                return Error();

            var success = await _service.UpdateIdentityResourceAsync(resource);
            if (success)
                return Success();
            return Error();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.RemoveIdentityResourceByIdAsync(id);
            if (success)
                return RedirectToAction("Index");

            TempData["ErrorMessage"] = "Cannot delete Identity Resource. Please try again!";
            return RedirectToAction("Index");
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