using OrgCore.Domain.Contexts.Identity.Entities;

namespace OrgCore.Domain.Interfaces
{
    public interface IColaboradorRepository
    {
        Task Vincular(Colaborador colaborador);
        // TODO: Desativar colaborador
        Task<bool> Commit();
    }
}
