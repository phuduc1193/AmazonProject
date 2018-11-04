using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using AuthService.Helpers;
using AuthService.Services;
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
            apiResource = AddScopeIfEmpty(apiResource);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] ApiResource apiResource)
        {
            apiResource = AddScopeIfEmpty(apiResource);
            var success = await _service.UpdateApiResourceAsync(apiResource);
            if (success)
                return Success();
            return Error();
        }


        private static ApiResource AddScopeIfEmpty(ApiResource apiResource)
        {
            if (apiResource.Scopes == null || apiResource.Scopes.Count == 0
                || string.IsNullOrWhiteSpace(apiResource.Scopes.First().Name))
            {
                apiResource.Scopes = new List<ApiScope> {
                    new ApiScope {
                        Name = apiResource.Name,
                        DisplayName = apiResource.DisplayName,
                        Description = apiResource.Description,
                        Emphasize = true,
                        Required = true
                    }
                };
            }
            return apiResource;
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