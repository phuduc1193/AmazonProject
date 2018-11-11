using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace AuthService.Common.Interfaces.Services
{
    public interface IClientService
    {
        Task<Client> GetClientByIdAsync(int id);
        Task<List<Client>> GetListClientsAsync();
        Task<bool> AddClientAsync(Client client);
        Task<bool> UpdateClientAsync(Client client);
        Task<bool> RemoveClientByIdAsync(int id);
    }
}
