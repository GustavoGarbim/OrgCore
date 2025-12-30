using OrgCore.Application.DTOs.Pessoa;
using OrgCore.Application.Interfaces;
using OrgCore.Domain.Contexts.Identity.Entities;
using OrgCore.Domain.Interfaces;
using OrgCore.Infrastructure.Repositories;

namespace OrgCore.Application.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IDocValidator _cpfValidator;
        private readonly PessoaRepository _pessoaRepository;

        public PessoaService(IDocValidator cpfValidator, PessoaRepository pessoaRepository)
        {
            _cpfValidator = cpfValidator;
            _pessoaRepository = pessoaRepository;
        }

        public async Task<Pessoa> RegistrarPessoa(CriarPessoaDto dto)
        {
            var cpfValido = await _cpfValidator.ValidarPessoa(dto.Cpf);
            if(!cpfValido)
            {
                throw new Exception("CPF Inválido.");
            }

            var pessoa = new Pessoa(dto.Nome, dto.Cpf, dto.EmailPessoal);
            await _pessoaRepository.Adicionar(pessoa);
            await _pessoaRepository.Commit();

            return pessoa;
        }

        public async Task<Pessoa?> ObterPessoaPorId(Guid id)
        {
            return await _pessoaRepository.ObterPessoaPorId(id);
        }
    }
}
