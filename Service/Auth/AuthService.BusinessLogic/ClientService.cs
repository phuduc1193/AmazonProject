using AuthService.Common.Interfaces.Repositories;
using AuthService.Common.Interfaces.Services;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic
{
    public class ClientService : IClientService
    {
        private IClientRepository _repo;

        public ClientService(IClientRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> AddClientAsync(Client client)
        {
            var results = await _repo.AddClientAsync(client);
            return results > 0;
        }

        public async Task<List<Client>> GetListClientsAsync()
        {
            var results = await _repo.GetListClientsAsync();
            return results;
        }
    }
}
