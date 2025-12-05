using Microsoft.EntityFrameworkCore;
using OrgCore.Domain.Evaluation.Entities;
using OrgCore.Domain.Interfaces;
using OrgCore.Infrastructure.Context;

namespace OrgCore.Infrastructure.Repositories
{
    public class FormularioRepository : IFormularioRepository
    {
        private readonly OrgCoreDbContext _context;

        public FormularioRepository(OrgCoreDbContext context)
        {
            _context = context;
        }

        public async Task Adicionar(FormularioTemplate formulario)
        {
            await _context.Formularios.AddAsync(formulario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FormularioTemplate>> PegarFormulariosPorEmpresaAsync(Guid id)
        {
            var empresa = await _context.Formularios.Where(f => f.EmpresaId == id)
                .Include(f => f.Secoes)
                .ThenInclude(s => s.Questoes)
                .Where(f => f.EmpresaId == id)
                .AsNoTracking()
                .ToListAsync();

            return empresa;
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
