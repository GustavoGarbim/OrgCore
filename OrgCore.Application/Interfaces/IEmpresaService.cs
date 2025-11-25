using OrgCore.Application.DTOs;
using OrgCore.Domain.Contexts.Identity.Entities;

namespace OrgCore.Application.Interfaces
{
    public interface IEmpresaService
    {
        Task<Guid> RegistrarEmpresa(NovaEmpresaDto empresaDto);
        Task<List<Empresa>> ObterTodasEmpresas();
    }
}
