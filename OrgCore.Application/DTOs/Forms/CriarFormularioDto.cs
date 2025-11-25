namespace OrgCore.Application.DTOs.Forms
{
    public class CriarFormularioDto
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid EmpresaId { get; set; }
        public List<CriarSecaoDto> Secoes { get; set; } = new();
    }
}
