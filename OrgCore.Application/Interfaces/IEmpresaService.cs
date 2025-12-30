using OrgCore.Application.DTOs.Empresa;
using OrgCore.Domain.Contexts.Identity.Entities;

namespace OrgCore.Application.Interfaces
{
    public interface IEmpresaService
    {
        Task AtribuirCargo(AtribuirCargoDTO dto);
        Task<Guid> RegistrarEmpresa(EmpresaDTO empresaDto);
        Task<List<Empresa>> ObterTodasEmpresas();
    }
}
