using Moq;
using Xunit;
using OrgCore.Application.Services;
using OrgCore.Application.DTOs.Pessoa;
using OrgCore.Domain.Interfaces;
using OrgCore.Domain.Contexts.Identity.Entities;

namespace OrgCore.Tests.Application
{
    public class PessoaServiceTests
    {
        private readonly Mock<IDocValidator> _docValidatorMock;
        private readonly Mock<IPessoaRepository> _pessoaRepositoryMock;
        private readonly PessoaService _pessoaService;

        public PessoaServiceTests()
        {
            _docValidatorMock = new Mock<IDocValidator>();
            _pessoaRepositoryMock = new Mock<IPessoaRepository>();
            _pessoaService = new PessoaService(_docValidatorMock.Object, _pessoaRepositoryMock.Object);
        }

        [Fact]
        public async Task RegistrarPessoa_DeveRetornarPessoa_QuandoDadosValidos()
        {
            var dto = new CriarPessoaDto { Nome = "Teste", Cpf = "123", EmailPessoal = "teste@email.com" };
            _docValidatorMock.Setup(v => v.ValidarPessoa(dto.Cpf)).ReturnsAsync(true);
            
            _pessoaRepositoryMock.Setup(r => r.Adicionar(It.IsAny<Pessoa>())).Returns(Task.CompletedTask);
            _pessoaRepositoryMock.Setup(r => r.Commit()).ReturnsAsync(true);

            var resultado = await _pessoaService.RegistrarPessoa(dto);

            Assert.NotNull(resultado);
            Assert.Equal(dto.Nome, resultado.Nome);
            _pessoaRepositoryMock.Verify(r => r.Adicionar(It.IsAny<Pessoa>()), Times.Once);
            _pessoaRepositoryMock.Verify(r => r.Commit(), Times.Once);
        }

        [Fact]
        public async Task RegistrarPessoa_DeveLancarException_QuandoCpfInvalido()
        {
            var dto = new CriarPessoaDto { Nome = "Teste", Cpf = "123", EmailPessoal = "teste@email.com" };
            _docValidatorMock.Setup(v => v.ValidarPessoa(dto.Cpf)).ReturnsAsync(false);

            await Assert.ThrowsAsync<ArgumentException>(() => _pessoaService.RegistrarPessoa(dto));
            _pessoaRepositoryMock.Verify(r => r.Adicionar(It.IsAny<Pessoa>()), Times.Never);
        }
    }
}
