using OrgCore.Domain.Contexts.Identity.Entities;
using OrgCore.Domain.Evaluation.Enums;

namespace OrgCore.Domain.Interfaces
{
    public interface IColaboradorRepository
    {
        Task Vincular(Colaborador colaborador);
        Task<IEnumerable<Colaborador>> ObterColaboradoresPorEmpresaId(Guid empresaId);
        Task<Colaborador?> ObterPorId(Guid colaboradorId);
        Task<EnumCargo> PegarCargoDoUsuarioLogado(Guid colaboradorId);
        Task<bool> Commit();
    }
}
