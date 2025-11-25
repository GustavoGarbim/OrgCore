using OrgCore.Domain.Common;

namespace OrgCore.Domain.Evaluation.Entities
{
    public class FormularioTemplate : Entity
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public Guid EmpresaId { get; private set; }

        private readonly List<Secao> _secoes = new();
        public virtual IReadOnlyCollection<Secao> Secoes => _secoes.AsReadOnly();

        public FormularioTemplate(string titulo, string descricao, Guid empresaId)
        {
            Titulo = titulo;
            Descricao = descricao;
            EmpresaId = empresaId;
        }

        public Secao AdicionarSecao(string titulo, int ordem)
        {
            var secao = new Secao(titulo, ordem);
            _secoes.Add(secao);
            return secao;
        }

        protected FormularioTemplate() { }
    }
}
