using OrgCore.Domain.Contexts.Identity.Entities;
using OrgCore.Infrastructure.Context;

namespace OrgCore.Infrastructure.Repositories
{
    public class ColaboradorRepository
    {
        private readonly OrgCoreDbContext _context;
        public ColaboradorRepository(OrgCoreDbContext context) => _context = context;

        public async Task Adicionar(Colaborador colaborador)
        {
            await _context.Colaboradores.AddAsync(colaborador);
        }
    }
}
