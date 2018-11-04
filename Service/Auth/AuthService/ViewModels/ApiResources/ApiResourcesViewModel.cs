using IdentityServer4.EntityFramework.Entities;
using System.Collections.Generic;

namespace AuthService.ViewModels
{
    public class ApiResourcesViewModel
    {
        public List<ApiResource> ApiResources { get; set; }
    }
}
