using System;
using System.Collections.Generic;
using System.Linq;
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
using Newtonsoft.Json.Serialization;

namespace AuthService.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = Const.DefaultRoles.Admin)]
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
        public async Task<IActionResult> New([FromBody] ApiResourceInputModel model)
        {
            var apiResource = BuildApiResourceFromModel(model);
            var success = await _service.AddApiResourceAsync(apiResource);
            if (success)
                return Success();
            return Error();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var apiResource = await _service.GetApiResourceByIdAsync(id);
            var model = BuildApiResourceViewModel(apiResource);
            var jsonString = JsonConvert.SerializeObject(model, Formatting.None, jsonSerializerSettings);
            ViewData["Data"] = jsonString;
            return View();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] ApiResourceInputModel model)
        {
            var apiResource = BuildApiResourceFromModel(model);
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

        private ApiResourceInputModel BuildApiResourceViewModel(ApiResource apiResource)
        {
            var result = new ApiResourceInputModel
            {
                Id = apiResource.Id,
                Name = apiResource.Name,
                DisplayName = apiResource.DisplayName,
                Description = apiResource.Description,
                Secret = apiResource.Secrets.FirstOrDefault()?.Value,
                Enabled = apiResource.Enabled
            };

            var scopes = new List<ScopeInputModel>();
            apiResource.Scopes.ForEach(s =>
            {
                var scope = new ScopeInputModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    DisplayName = s.DisplayName,
                    Description = s.Description,
                    Required = s.Required,
                    Emphasize = s.Emphasize,
                    ShowInDiscoveryDocument = s.ShowInDiscoveryDocument
                };
                scopes.Add(scope);
            });
            result.Scopes = scopes;

            var claims = new List<ClaimInputModel>();
            apiResource.UserClaims.ForEach(c =>
            {
                claims.Add(new ClaimInputModel { Id = c.Id, Type = c.Type, Checked = true });
            });
            result.Claims = claims;

            return result;
        }
        private ApiResource BuildApiResourceFromModel(ApiResourceInputModel model)
        {
            var result = new ApiResource
            {
                Id = model.Id,
                Name = model.Name,
                DisplayName = model.DisplayName,
                Description = model.Description,
                Enabled = model.Enabled,
                Secrets = new List<ApiSecret>
                {
                    new ApiSecret { Value = model.Secret, Description = $"Secret for {model.Name}" }
                }
            };

            var scopes = new List<ApiScope>();
            if (model.Scopes == null || model.Scopes.Count == 0)
            {
                var scope = BuildDefaultApiScope(model);
                scopes.Add(scope);
            }
            else
            {
                foreach (var inputScope in model.Scopes)
                {
                    var scope = BuildApiScopeFromInput(inputScope);
                    scopes.Add(scope);
                }
            }
            result.Scopes = scopes;

            if (model.Claims != null && model.Claims.Count != 0)
            {
                var claims = new List<ApiResourceClaim>();
                foreach (var inputClaim in model.Claims)
                {
                    var claim = new ApiResourceClaim
                    {
                        Id = inputClaim.Id,
                        Type = inputClaim.Type
                    };
                    claims.Add(claim);
                }
                result.UserClaims = claims;
            }

            return result;
        }
        private static ApiScope BuildApiScopeFromInput(ScopeInputModel model)
        {
            return new ApiScope
            {
                Id = model.Id,
                Name = model.Name,
                DisplayName = model.DisplayName,
                Description = model.Description,
                Emphasize = model.Emphasize,
                Required = model.Required,
                ShowInDiscoveryDocument = model.ShowInDiscoveryDocument
            };
        }
        private static ApiScope BuildDefaultApiScope(ApiResourceInputModel model)
        {
            return new ApiScope
            {
                Name = model.Name,
                DisplayName = model.DisplayName,
                Description = model.Description,
                Emphasize = true,
                Required = false,
                ShowInDiscoveryDocument = false
            };
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