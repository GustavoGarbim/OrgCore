using OrgCore.Domain.Contexts.Identity.Entities;
using OrgCore.Domain.Interfaces;
using OrgCore.Infrastructure.Context;

namespace OrgCore.Infrastructure.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly OrgCoreDbContext _context;

        public EmpresaRepository(OrgCoreDbContext context)
        {
            _context = context;
        }

        public async Task Adicionar(Empresa empresa)
        {
            await _context.Empresas.AddAsync(empresa);
        }

        public async Task<List<Empresa>> ObterTodas()
        {
            return await Task.FromResult(_context.Empresas.ToList());
        }

        public async Task<Empresa?> ObterEmpresaPorId(Guid id)
        {
            return await _context.Empresas.FindAsync(id);
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
