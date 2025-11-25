using OrgCore.Domain.Common;
using OrgCore.Domain.Evaluation.Enums;

namespace OrgCore.Domain.Evaluation.Entities
{
    public class Secao : Entity
    {
        public string Titulo { get; private set; }
        public int Ordem { get; private set; }
        public Guid FormularioId { get; private set; }

        private readonly List<Questao> _questoes = new();
        public virtual IReadOnlyCollection<Questao> Questoes => _questoes.AsReadOnly();

        public Secao(string titulo, int ordem)
        {
            Titulo = titulo;
            Ordem = ordem;
        }

        public void AdicionarQuestao(string enunciado, TipoQuestao tipo, bool obrigatoria)
        {
            var questao = new Questao(enunciado, tipo, obrigatoria);
            _questoes.Add(questao);
        }

        protected Secao() { }
    }
}
