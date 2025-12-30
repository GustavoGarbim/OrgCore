using OrgCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Domain.Evaluation.Entities
{
    public class Avaliacao : Entity
    {
        public Guid FormularioId { get; set; }
        public Guid ColaboradorId { get; set; }
        public DateTime DataResposta { get; set; } = DateTime.UtcNow;

        private readonly List<Resposta> _respostas = new();
        public virtual IReadOnlyCollection<Resposta> Respostas => _respostas.AsReadOnly();

        public Avaliacao(Guid formularioId, Guid colaboradorId)
        {
            FormularioId = formularioId;
            ColaboradorId = colaboradorId;
        }

        public void AdicionarResposta(Guid questaoId, string valor)
        {
            _respostas.Add(new Resposta(questaoId, valor));
        }

        protected Avaliacao() { }
    }
}
