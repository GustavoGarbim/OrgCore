using Microsoft.EntityFrameworkCore;
using OrgCore.Domain.Contexts.Identity.Entities;
using OrgCore.Domain.Evaluation.Enums;
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

        public async Task<IEnumerable<Colaborador>> ObterColaboradoresPorEmpresaId(Guid empresaId)
        {
            return _context.Colaboradores
                .AsNoTracking()
                .Include(c => c.Empresa)
                .Include(c => c.Pessoa)
                .ToList()
                .Where(c => c.EmpresaId == empresaId)
                .ToList();
        }

        public async Task<Colaborador?> ObterPorId(Guid colaboradorId)
        {
            return await _context.Colaboradores
                .Include(c => c.Empresa)
                .Include(c => c.Pessoa)
                .FirstOrDefaultAsync(c => c.Id == colaboradorId);
        }

        public async Task<EnumCargo> PegarCargoDoUsuarioLogado(Guid colaboradorId)
        {
            return await _context.Colaboradores
                .Where(u => u.Id == colaboradorId)
                .Select(u => u.Cargo)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
