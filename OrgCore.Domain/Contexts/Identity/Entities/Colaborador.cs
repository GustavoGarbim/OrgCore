using OrgCore.Domain.Common;

namespace OrgCore.Domain.Contexts.Identity.Entities
{
    public class Colaborador : Entity
    {
        public Guid EmpresaId { get; private set; }
        public Guid PessoaId { get; private set; }
        public bool IsGestor { get; private set; }

        public virtual Empresa Empresa { get; private set; }
        public virtual Pessoa Pessoa { get; private set; }

        public Colaborador() { }

        public Colaborador(Guid empresaId, Guid pessoaId)
        {
            EmpresaId = empresaId;
            PessoaId = pessoaId;
            IsGestor = false;
        }

        public void TornarGestor()
        {
            IsGestor = true;
        }
    }
}
