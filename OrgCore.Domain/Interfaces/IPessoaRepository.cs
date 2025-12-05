using OrgCore.Domain.Contexts.Identity.Entities;

namespace OrgCore.Domain.Interfaces
{
    public interface IPessoaRepository
    {
        Task Adicionar(Pessoa pessoa);
        Task<Pessoa?> ObterPessoaPorId(Guid id);
        Task<bool> Commit();
    }
}
