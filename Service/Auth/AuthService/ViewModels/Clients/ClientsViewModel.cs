using IdentityServer4.EntityFramework.Entities;
using System.Collections.Generic;

namespace AuthService.ViewModels
{
    public class ClientsViewModel
    {
        public List<Client> Clients { get; set; }
    }
}
