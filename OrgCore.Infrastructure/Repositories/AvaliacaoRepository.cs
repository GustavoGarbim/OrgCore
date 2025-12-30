using Microsoft.EntityFrameworkCore;
using OrgCore.Domain.Evaluation.Entities;
using OrgCore.Domain.Interfaces;
using OrgCore.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Infrastructure.Repositories
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private readonly OrgCoreDbContext _context;
        public AvaliacaoRepository(OrgCoreDbContext context)
        {
            _context = context;
        }

        public async Task Adicionar(Avaliacao avaliacao)
        {
            await _context.Avaliacoes.AddAsync(avaliacao);
        }

        public async Task<Avaliacao?> ObterComRespostas(Guid id)
        {
            return await _context.Avaliacoes
                                 .Include(a => a.Respostas)
                                 .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
