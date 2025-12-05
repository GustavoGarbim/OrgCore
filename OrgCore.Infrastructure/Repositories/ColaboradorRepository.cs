using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using OrgCore.Domain.Contexts.Identity.Entities;
using OrgCore.Domain.Interfaces;
using OrgCore.Infrastructure.Context;

namespace OrgCore.Infrastructure.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly OrgCoreDbContext _context;
        public ColaboradorRepository(OrgCoreDbContext context) => _context = context;

        public async Task Vincular(Colaborador colaborador)
        {
            await _context.Colaboradores.AddAsync(colaborador);

        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
