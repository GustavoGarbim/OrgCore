using OrgCore.Domain.Evaluation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Domain.Interfaces
{
    public interface IAvaliacaoRepository
    {
        Task Adicionar(Avaliacao avaliacao);
        Task<Avaliacao?> ObterComRespostas(Guid id);
        Task<bool> Commit();
    }
}
