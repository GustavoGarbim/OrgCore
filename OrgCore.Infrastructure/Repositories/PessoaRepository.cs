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
    }
}
