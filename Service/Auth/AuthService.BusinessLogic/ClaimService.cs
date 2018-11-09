using AuthService.Common.Interfaces.Repositories;
using AuthService.Common.Interfaces.Services;

namespace AuthService.BusinessLogic
{
    public class ClaimService : IClaimService
    {
        private IClaimRepository _repo;

        public ClaimService(IClaimRepository repo)
        {
            _repo = repo;
        }
    }
}
