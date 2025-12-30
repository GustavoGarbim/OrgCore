using OrgCore.Application.DTOs.Forms;
using OrgCore.Application.Interfaces;
using OrgCore.Domain.Evaluation.Entities;
using OrgCore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Application.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _repository;
        private readonly IFormularioRepository _formularioRepository;

        public AvaliacaoService(IAvaliacaoRepository repository, IFormularioRepository formularioRepository)
        {
            _repository = repository;
            _formularioRepository = formularioRepository;
        }

        public async Task<Guid> EnviarResposta(RealizarAvaliacaoDto dto)
        {
            var avaliacao = new Avaliacao(dto.FormularioId, dto.ColaboradorId);

            foreach (var item in dto.Respostas)
            {
                avaliacao.AdicionarResposta(item.QuestaoId, item.Valor);
            }

            await _repository.Adicionar(avaliacao);
            await _repository.Commit();

            return avaliacao.Id;
        }

        public async Task<VisualizarAvaliacaoDto> ObterRelatorio(Guid id)
        {
            var avaliacao = await _repository.ObterComRespostas(id);
            if (avaliacao == null)
                throw new Exception("Avaliação não encontrada.");
            var formulario = await _formularioRepository.ObterPorId(avaliacao.FormularioId);

            var dto = new VisualizarAvaliacaoDto
            {
                Id = avaliacao.Id,
                DataResposta = avaliacao.DataResposta,
                TituloFormulario = formulario.Titulo,
                NomeColaborador = "João",
            };

            var todasAsQuestoes = formulario.Secoes.SelectMany(s => s.Questoes).ToList();

            foreach (var resposta in avaliacao.Respostas)
            { 
                var questaoOriginal = todasAsQuestoes.FirstOrDefault(q => q.Id == resposta.QuestaoId);

                dto.Respostas.Add(new DetalheRespostaDto
                {
                    ValorResposta = resposta.Valor,
                    EnunciadoQuestao = questaoOriginal != null ? questaoOriginal.Enunciado : "Questão não encontrada",
                    Tipo = questaoOriginal?.Tipo.ToString() ?? "Desconhecido"
                });
            }
            return dto;
        }
    }
}
