using OrgCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Domain.Evaluation.Entities
{
    public class Resposta : Entity
    {
        public Guid QuestaoId { get; private set; }
        public string Valor { get; private set; }

        public Resposta(Guid questaoId, string valor) 
        {
            QuestaoId = questaoId;
            Valor = valor;
        }

        protected Resposta() { }
    }
}
