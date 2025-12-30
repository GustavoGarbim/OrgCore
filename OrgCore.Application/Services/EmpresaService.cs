using OrgCore.Application.DTOs.Empresa;
using OrgCore.Application.Interfaces;
using OrgCore.Domain.Contexts.Identity.Entities;
using OrgCore.Domain.Interfaces;

namespace OrgCore.Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IColaboradorRepository _colaboradorRepository;
        private readonly IDocValidator _cnpjValidator;

        public EmpresaService(IEmpresaRepository empresaRepository, IPessoaRepository pessoaRepository, IColaboradorRepository colaboradorRepository, IDocValidator cnpjValidator)
        {
            _empresaRepository = empresaRepository;
            _pessoaRepository = pessoaRepository;
            _colaboradorRepository = colaboradorRepository;
            _cnpjValidator = cnpjValidator;
        }

        public async Task<Guid> RegistrarEmpresa(EmpresaDTO dto)
        {
            var cnpjValido = await _cnpjValidator.ValidarEmpresa(dto.Cnpj);
            if (!cnpjValido)
                throw new Exception("CNPJ inválido.");

            var empresa = new Empresa(dto.NomeFantasia, dto.Cnpj);
            await _empresaRepository.Adicionar(empresa);

            var pessoa = new Pessoa(dto.NomeAdmin, dto.EmailAdmin);
            await _pessoaRepository.Adicionar(pessoa);

            var colaborador = new Colaborador(empresa.Id, pessoa.Id);
            colaborador.TornarGestor();
            await _colaboradorRepository.Vincular(colaborador);

            await _empresaRepository.Commit();

            return empresa.Id;
        }

        public async Task AtribuirCargo(AtribuirCargoDTO dto)
        {
            var colaborador = await _colaboradorRepository.ObterPorId(dto.ColaboradorId);
            if (colaborador == null)
            {
                throw new Exception("Colaborador não encontrado.");
            }
            colaborador.AtribuirCargo(dto.Cargo);
            await _colaboradorRepository.Commit();
        }

        public async Task<List<Empresa>> ObterTodasEmpresas()
        {
            return await _empresaRepository.ObterTodas();
        }
    }
}
