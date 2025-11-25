using OrgCore.Domain.Common;
using OrgCore.Domain.Evaluation.Enums;

namespace OrgCore.Domain.Evaluation.Entities
{
    public class Questao : Entity
    {
        public string Enunciado { get; private set; }
        public TipoQuestao Tipo { get; private set; }
        public bool Obrigatoria { get; private set; }
        public Guid SecaoId { get; private set; }

        public Questao(string enunciado, TipoQuestao tipo, bool obrigatoria)
        {
            Enunciado = enunciado;
            Tipo = tipo;
            Obrigatoria = obrigatoria;
        }

        protected Questao() { }
    }
}
