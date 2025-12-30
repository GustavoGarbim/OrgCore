using Microsoft.EntityFrameworkCore;
using OrgCore.Domain.Contexts.Identity.Entities;
using OrgCore.Domain.Interfaces;
using OrgCore.Infrastructure.Context;

namespace OrgCore.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly OrgCoreDbContext _context;

        public AuthRepository(OrgCoreDbContext context)
        {
            _context = context;
        }
        
        public async Task<Pessoa?> GerarTokenAcesso(string cpf)
        {
            return await _context.Pessoas
                         .AsNoTracking()
                         .FirstOrDefaultAsync(p => p.Cpf == cpf);
        }
    }
}
