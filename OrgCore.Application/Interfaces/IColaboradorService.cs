using OrgCore.Application.DTOs;

namespace OrgCore.Application.Interfaces
{
    public interface IColaboradorService
    {
        Task VincularColaborador(VincularColaboradorDto vincularDto);
    }
}
