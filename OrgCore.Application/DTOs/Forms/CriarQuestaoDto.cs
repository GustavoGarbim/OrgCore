using OrgCore.Domain.Evaluation.Enums;

namespace OrgCore.Application.DTOs.Forms
{
    public class CriarQuestaoDto
    {
        public string Enunciado { get; set; }
        public TipoQuestao Tipo { get; set; }
        public bool Obrigatorio { get; set; }
    }
}
