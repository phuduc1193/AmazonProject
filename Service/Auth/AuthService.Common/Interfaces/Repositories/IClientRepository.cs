using IdentityServer4.EntityFramework.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Common.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<List<Client>> GetListClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<int> AddClientAsync(Client client);
        Task<int> UpdateClientAsync(Client client);
        Task<int> RemoveClientByIdAsync(int id);
    }
}
