using OrgCore.Application.DTOs;
using OrgCore.Application.Interfaces;
using OrgCore.Domain.Contexts.Identity.Entities;
using OrgCore.Domain.Interfaces;
using OrgCore.Infrastructure.Repositories;

namespace OrgCore.Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly PessoaRepository _pessoaRepository;
        private readonly ColaboradorRepository _colaboradorRepository;
        private readonly ICnpjValidator _cnpjValidator;

        public EmpresaService(IEmpresaRepository empresaRepository, PessoaRepository pessoaRepository, ColaboradorRepository colaboradorRepository, ICnpjValidator cnpjValidator)
        {
            _empresaRepository = empresaRepository;
            _pessoaRepository = pessoaRepository;
            _colaboradorRepository = colaboradorRepository;
            _cnpjValidator = cnpjValidator;
        }

        public async Task<Guid> RegistrarEmpresa(NovaEmpresaDto dto)
        {
            var cnpjValido = await _cnpjValidator.ValidarEmpresa(dto.Cnpj);
            if (!cnpjValido)
                throw new Exception("CNPJ inválido ou já cadastrado.");

            var empresa = new Empresa(dto.NomeFantasia, dto.Cnpj);
            await _empresaRepository.Adicionar(empresa);

            var pessoa = new Pessoa(dto.NomeAdmin, dto.EmailAdmin);
            await _pessoaRepository.Adicionar(pessoa);

            var colaborador = new Colaborador(empresa.Id, pessoa.Id);
            colaborador.TornarGestor();
            await _colaboradorRepository.Adicionar(colaborador);

            await _empresaRepository.Commit();

            return empresa.Id;
        }

        public async Task<List<Empresa>> ObterTodasEmpresas()
        {
            return await _empresaRepository.ObterTodas();
        }
    }
}
