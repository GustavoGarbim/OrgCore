using OrgCore.Application.DTOs.Forms;
using OrgCore.Application.Interfaces;
using OrgCore.Domain.Evaluation.Entities;
using OrgCore.Domain.Interfaces;

namespace OrgCore.Application.Services
{
    public class FormularioService : IFormularioService
    {
        private readonly IFormularioRepository _formularioRepository;
        private readonly IColaboradorRepository _colaboradorRepository;

        public FormularioService(IFormularioRepository formularioRepository, IColaboradorRepository colaboradorRepository)
        {
            _formularioRepository = formularioRepository;
            _colaboradorRepository = colaboradorRepository;
        }

        public async Task<Guid> CriarTemplate(CriarFormularioDto dto)
        {
            var cargoUsuario = await _colaboradorRepository.PegarCargoDoUsuarioLogado(dto.IdUsuarioCriador);
            if(cargoUsuario != Domain.Evaluation.Enums.EnumCargo.Gerente)
            {
                throw new Exception("Usuario não possui o cargo necessário.");
            }

            var form = new FormularioTemplate(dto.Titulo, dto.Descricao, dto.EmpresaId);

            foreach (var secaoDto in dto.Secoes)
            {
                var novaSecao = form.AdicionarSecao(secaoDto.Titulo, secaoDto.Ordem);

                foreach (var questaoDto in secaoDto.Questoes)
                {
                    novaSecao.AdicionarQuestao(
                        questaoDto.Enunciado,
                        questaoDto.Tipo,
                        questaoDto.Obrigatorio
                        );
                }
            }

            await _formularioRepository.Adicionar(form);
            await _formularioRepository.Commit();

            return form.Id;
        }

        public async Task<List<FormularioTemplate>> PegarFormulariosPorEmpresaAsync(Guid empresaId)
        {
            return await _formularioRepository.PegarFormulariosPorEmpresaAsync(empresaId);
        }
    }
}
