using OrgCore.Application.DTOs.Pessoa;
using OrgCore.Domain.Contexts.Identity.Entities;

namespace OrgCore.Application.Interfaces
{
    public interface IPessoaService
    {
        Task<Pessoa> RegistrarPessoa(CriarPessoaDto criarPessoaDto);
        Task<Pessoa> ObterPessoaPorId(Guid id);
    }
}
