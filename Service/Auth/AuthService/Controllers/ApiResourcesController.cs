using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Helpers;
using AuthService.Services;
using AuthService.ViewModels;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult New(NewApiResourceInputModel model)
        {
            var vm = new NewApiResourceViewModel
            {
                Scopes = new List<NewApiResourceScopeViewModel>()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(NewApiResourceInputModel model, string button)
        {
            if (string.Equals(button, "scope", StringComparison.InvariantCultureIgnoreCase))
            {
                var vm = BuildApiResourceViewModel(model);
                return View(vm);
            }

            var apiResource = BuildApiResourceModel(model);
            var success = await _service.SaveApiResourceAsync(apiResource);
            if (!success)
                return View();
            return RedirectToAction("Index");
        }

        private NewApiResourceViewModel BuildApiResourceViewModel(NewApiResourceInputModel model)
        {
            var vm = new NewApiResourceViewModel
            {
                Name = model.Name,
                DisplayName = model.DisplayName,
                Description = model.Description,
                Scopes = !string.IsNullOrWhiteSpace(model.SerializedScopes) ?
                    DeserializeListScopes(model.SerializedScopes) : new List<NewApiResourceScopeViewModel>()
            };

            if (!string.IsNullOrWhiteSpace(model.ScopeName)
                || !string.IsNullOrWhiteSpace(model.ScopeDisplayName)
                || !string.IsNullOrWhiteSpace(model.ScopeDescription))
            {
                var scope = new NewApiResourceScopeViewModel
                {
                    ScopeName = model.ScopeName,
                    ScopeDisplayName = model.ScopeDisplayName,
                    ScopeDescription = model.ScopeDescription,
                    ScopeEmphasize = model.ScopeEmphasize,
                    ScopeRequired = model.ScopeRequired
                };
                vm.Scopes.Add(scope);
            }
            vm.SerializedScopes = JsonConvert.SerializeObject(vm.Scopes);
            return vm;
        }

        private static List<NewApiResourceScopeViewModel> DeserializeListScopes(string input)
        {
            return JsonConvert.DeserializeObject<List<NewApiResourceScopeViewModel>>(input);
        }

        private ApiResource BuildApiResourceModel(NewApiResourceInputModel model)
        {
            var apiResource = new ApiResource
            {
                Name = model.Name,
                DisplayName = model.DisplayName,
                Description = model.Description,
                Enabled = true,
                Scopes = new List<ApiScope>()
            };

            var scopes = DeserializeListScopes(model.SerializedScopes);
            if (scopes.Count == 0)
            {
                apiResource.Scopes.Add(new ApiScope() { Name = model.Name, DisplayName = model.DisplayName });
                return apiResource;
            }

            foreach (var scope in scopes)
            {
                apiResource.Scopes.Add(new ApiScope
                {
                    Name = scope.ScopeName,
                    DisplayName = scope.ScopeDisplayName,
                    Description = scope.ScopeDescription,
                    Emphasize = scope.ScopeEmphasize,
                    Required = scope.ScopeRequired,
                    ShowInDiscoveryDocument = true
                });
            }
            return apiResource;
        }
    }
}