using OrgCore.Application.DTOs.Empresa.Colaborador;
using OrgCore.Domain.Contexts.Identity.Entities;

namespace OrgCore.Application.Interfaces
{
    public interface IColaboradorService
    {
        Task VincularColaborador(CadastrarColaboradorDTO vincularDto);
        Task<IEnumerable<ColaboradorDTO>> ObterColaboradoresPorEmpresaId(Guid empresaId);
        Task<Colaborador?> ObterColaboradorPorId(Guid colaboradorId);
    }
}
