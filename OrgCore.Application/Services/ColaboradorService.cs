using OrgCore.Application.DTOs;
using OrgCore.Application.Interfaces;
using OrgCore.Domain.Contexts.Identity.Entities;
using OrgCore.Domain.Interfaces;

namespace OrgCore.Application.Services
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly IColaboradorRepository _colaboradorRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public ColaboradorService(IColaboradorRepository colaboradorRepository, IPessoaRepository pessoaRepository, IEmpresaRepository empresaRepository)
        {
            _colaboradorRepository = colaboradorRepository;
            _pessoaRepository = pessoaRepository;
            _empresaRepository = empresaRepository;
        }

        public async Task VincularColaborador(VincularColaboradorDto dto)
        {
            var empresa = await _empresaRepository.ObterEmpresaPorId(dto.EmpresaId);
            if (empresa == null) throw new ArgumentException($"Empresa não encontrada: {dto.EmpresaId}", nameof(dto.EmpresaId));

            var pessoa = await _pessoaRepository.ObterPessoaPorId(dto.PessoaId);
            if (pessoa == null) throw new ArgumentException($"Pessoa não encontrada: {dto.PessoaId}", nameof(dto.PessoaId));

            var colaborador = new Colaborador(empresa.Id, pessoa.Id);

            await _colaboradorRepository.Vincular(colaborador);
            await _colaboradorRepository.Commit();
        }
    }
}
