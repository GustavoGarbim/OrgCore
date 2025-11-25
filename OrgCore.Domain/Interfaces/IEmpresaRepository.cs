using OrgCore.Domain.Contexts.Identity.Entities;

namespace OrgCore.Domain.Interfaces
{
    public interface IEmpresaRepository
    {
        Task Adicionar(Empresa empresa);
        Task<Empresa?> ObterEmpresaPorId(Guid id);

        Task<List<Empresa>> ObterTodas();

        Task<bool> Commit();
    }
}
