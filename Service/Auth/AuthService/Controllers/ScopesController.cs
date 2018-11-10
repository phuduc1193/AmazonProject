using AuthService.ViewModels.Scopes;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ScopesController : ControllerBase
    {
        private readonly IResourceStore _resources;

        public ScopesController(IResourceStore resources)
        {
            _resources = resources;
        }

        public async Task<ScopesViewModel> List()
        {
            var resources = await _resources.GetAllEnabledResourcesAsync();
            var result = new ScopesViewModel
            {
                ApiScopes = new List<string>(resources.ApiResources.Select(x => x.Name)),
                IdentityScopes = new List<string>(resources.IdentityResources.Select(x => x.Name))
            };
            return result;
        }
    }
}
