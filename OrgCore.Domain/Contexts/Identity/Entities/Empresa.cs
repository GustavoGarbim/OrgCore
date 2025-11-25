using OrgCore.Domain.Common;

namespace OrgCore.Domain.Contexts.Identity.Entities
{
    public class Empresa : Entity
    {
        public string NomeFantasia { get; private set; }
        public string Cnpj { get; private set; }
        public bool Ativo { get; private set; }

        public Empresa(string nomeFantasia, string cnpj)
        {
            NomeFantasia = nomeFantasia;
            Cnpj = cnpj.Replace(".", "")
                       .Replace("/", "")
                       .Replace("-", "")
                       .Replace(" ", "");
            Ativo = true;
        }

        public void AlterarNome(string novoNome)
        {
            NomeFantasia = novoNome;
        }
    }
}
