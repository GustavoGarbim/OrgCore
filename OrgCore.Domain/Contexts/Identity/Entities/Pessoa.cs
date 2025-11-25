using OrgCore.Domain.Common;

namespace OrgCore.Domain.Contexts.Identity.Entities
{
    public class Pessoa : Entity
    {
        public string Nome { get; private set; }
        public string EmailPessoal { get; private set; }
        public Guid? UsuarioId { get; private set; }

        public Pessoa(string nome, string emailPessoal)
        {
            Nome = nome;
            EmailPessoal = emailPessoal;
        }

        public void VincularUsuario(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
