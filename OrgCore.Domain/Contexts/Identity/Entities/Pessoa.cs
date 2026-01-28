using OrgCore.Domain.Common;

namespace OrgCore.Domain.Contexts.Identity.Entities
{
    public class Pessoa : Entity
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string EmailPessoal { get; private set; }
        public Guid? UsuarioId { get; private set; }

        protected Pessoa() { }

        public Pessoa(string nome, string emailPessoal)
        {
            Validar(nome, emailPessoal);
            Nome = nome;
            EmailPessoal = emailPessoal;
        }

        public Pessoa(string nome, string cpf, string emailPessoal)
        {
            Validar(nome, emailPessoal);
            
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF é obrigatório", nameof(cpf));

            Nome = nome;
            Cpf = cpf;
            EmailPessoal = emailPessoal;
        }

        public void VincularUsuario(Guid usuarioId)
        {
            if (usuarioId == Guid.Empty)
                throw new ArgumentException("ID de usuário inválido", nameof(usuarioId));

            UsuarioId = usuarioId;
        }

        private void Validar(string nome, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório", nameof(nome));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email é obrigatório", nameof(email));
        }
    }
}
