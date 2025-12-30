using OrgCore.Domain.Evaluation.Enums;

namespace OrgCore.Application.DTOs.Forms
{
    public class CriarFormularioDto
    {
        public Guid IdUsuarioCriador { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid EmpresaId { get; set; }
        public List<CriarSecaoDto> Secoes { get; set; } = new();
    }

    public class CriarQuestaoDto
    {
        public string Enunciado { get; set; }
        public TipoQuestao Tipo { get; set; }
        public bool Obrigatorio { get; set; }
    }

    public class CriarSecaoDto
    {
        public string Titulo { get; set; }
        public int Ordem { get; set; }
        public List<CriarQuestaoDto> Questoes { get; set; } = new();
    }

    public class RespostaItemDto
    {
        public Guid QuestaoId { get; set; }
        public string Valor { get; set; }
    }

    public class RealizarAvaliacaoDto
    {
        public Guid FormularioId { get; set; }
        public Guid ColaboradorId { get; set; }
        public List<RespostaItemDto> Respostas { get; set; }
    }

    public class VisualizarAvaliacaoDto
    {
        public Guid Id { get; set; }
        public string NomeColaborador { get; set; }
        public string TituloFormulario { get; set; }
        public DateTime DataResposta { get; set; }
        public List<DetalheRespostaDto> Respostas { get; set; } = new();
    }

    public class DetalheRespostaDto
    {
        public string EnunciadoQuestao { get; set; }
        public string ValorResposta { get; set; }
        public string Tipo { get; set; }
    }
}
