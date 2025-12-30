using OrgCore.Domain.Evaluation.Entities;

namespace OrgCore.Domain.Interfaces
{
    public interface IFormularioRepository
    {
        Task Adicionar(FormularioTemplate formulario);
        Task<List<FormularioTemplate>> PegarFormulariosPorEmpresaAsync(Guid id);
        Task<FormularioTemplate> ObterPorId(Guid id);
        Task<bool> Commit();
    }
}
