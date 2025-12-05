using OrgCore.Domain.Contexts.Identity.Entities;
using OrgCore.Domain.Interfaces;
using OrgCore.Infrastructure.Context;

namespace OrgCore.Infrastructure.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly OrgCoreDbContext _context;
        public PessoaRepository(OrgCoreDbContext context) => _context = context;

        public async Task Adicionar(Pessoa pessoa)
        {
            await _context.Pessoas.AddAsync(pessoa);
        }

        public async Task<Pessoa?> ObterPessoaPorId (Guid id)
        {
            return await _context.Pessoas.FindAsync(id);
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
