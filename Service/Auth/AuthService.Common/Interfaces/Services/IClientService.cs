using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace AuthService.Common.Interfaces.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetListClientsAsync();
    }
}
