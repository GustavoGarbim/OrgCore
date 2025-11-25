using OrgCore.Application.DTOs.Forms;
using OrgCore.Domain.Evaluation.Entities;

namespace OrgCore.Application.Interfaces
{
    public interface IFormularioService
    {
        Task<Guid> CriarTemplate(CriarFormularioDto dto);
        Task<List<FormularioTemplate>> PegarFormulariosPorEmpresaAsync(Guid empresaId);
    }
}
