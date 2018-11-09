using AuthService.Common.Interfaces.Contexts;
using AuthService.Common.Interfaces.Repositories;

namespace AuthService.DataAccess
{
    public class ClaimRepository : IClaimRepository
    {
        private IApplicationDbContext _context;

        public ClaimRepository(IApplicationDbContext context)
        {
            _context = context;
        }
    }
}
