using OrgCore.Application.DTOs;
using OrgCore.Application.Interfaces;
using OrgCore.Domain.Evaluation.Enums;
using OrgCore.Domain.Interfaces;

namespace OrgCore.Application.Services
{
    public class FuncaoTesteService : IFuncaoTeste
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public FuncaoTesteService(IColaboradorRepository colaboradorRepository)
        { 
            _colaboradorRepository = colaboradorRepository;
        }

        public async Task<EnumCargo> FuncaoTesteCargo(FuncaoTesteDTO dto)
        {
            var colaborador = await _colaboradorRepository.ObterPorId(dto.ColaboradorId);
            return colaborador != null ? colaborador.Cargo : throw new Exception("Colaborador não encontrado");
        }
    }
}
