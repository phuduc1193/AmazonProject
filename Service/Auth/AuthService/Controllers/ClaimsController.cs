using AuthService.Helpers;
using AuthService.ViewModels;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimsController : ControllerBase
    {
        [HttpGet]
        public List<ApiResourceClaimViewModel> List()
        {
            var claims = new List<ApiResourceClaimViewModel>();
            var fields = typeof(JwtClaimTypes).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var fieldInfo in fields)
            {
                var claim = new ApiResourceClaimViewModel
                {
                    Name = fieldInfo.Name.AddSpaceBeforeUppercase(),
                    Type = fieldInfo.GetValue(null).ToString()
                };
                claims.Add(claim);
            }
            return claims;
        }
    }
}
