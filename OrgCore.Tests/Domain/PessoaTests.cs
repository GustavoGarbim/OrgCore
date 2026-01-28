using Xunit;
using OrgCore.Domain.Contexts.Identity.Entities;

namespace OrgCore.Tests.Domain
{
    public class PessoaTests
    {
        [Fact]
        public void DeveCriarPessoaComSucesso()
        {
            var nome = "João da Silva";
            var email = "joao@email.com";

            var pessoa = new Pessoa(nome, email);

            Assert.NotNull(pessoa);
            Assert.Equal(nome, pessoa.Nome);
            Assert.Equal(email, pessoa.EmailPessoal);
            Assert.NotEqual(Guid.Empty, pessoa.Id);
            Assert.True(pessoa.DataCriacao <= DateTime.UtcNow);
        }

        [Theory]
        [InlineData("", "email@teste.com")]
        [InlineData(null, "email@teste.com")]
        [InlineData("Nome", "")]
        [InlineData("Nome", null)]
        public void DeveLancarErroQuandoDadosInvalidos(string nome, string email)
        {
            Assert.Throws<ArgumentException>(() => new Pessoa(nome, email));
        }

        [Fact]
        public void DeveLancarErroQuandoCpfInvalido()
        {
            Assert.Throws<ArgumentException>(() => new Pessoa("Nome", "", "email@email.com"));
        }
    }
}
