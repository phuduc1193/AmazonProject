using IdentityServer4.EntityFramework.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Common.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<List<Client>> GetListClientsAsync();
        Task<int> AddClientAsync(Client client);
    }
}
