using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
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
    //[Authorize(Roles = "Administrator")]
    [Authorize]
    public class ApiResourcesController : Controller
    {
        private IApiResourceService _service;

        public ApiResourcesController(IApiResourceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = new ApiResourcesViewModel { ApiResources = await _service.GetListApiResourcesAsync() };
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
        public async Task<IActionResult> New([FromBody] ApiResource apiResource)
        {
            var success = await _service.AddApiResourceAsync(apiResource);
            if (success)
                return Success();
            return Error();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var jsonSerializerSettings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var apiResource = await _service.GetApiResourceByIdAsync(id);
            var jsonString = JsonConvert.SerializeObject(apiResource, Formatting.None, jsonSerializerSettings);
            ViewData["Data"] = jsonString;
            return View();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] ApiResource apiResource)
        {
            var success = await _service.UpdateApiResourceAsync(apiResource);
            if (success)
                return Success();
            return Error();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.RemoveApiResourceByIdAsync(id);
            if (success)
                return RedirectToAction("Index");

            TempData["ErrorMessage"] = "Cannot delete API Resource. Please try again!";
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