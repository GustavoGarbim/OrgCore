using OrgCore.Domain.Evaluation.Enums;

namespace OrgCore.Application.DTOs.Empresa
{
    public class EmpresaDTO
    {
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string NomeAdmin { get; set; }
        public string EmailAdmin { get; set; }
    }

    public class  AtribuirCargoDTO
    {
        public Guid ColaboradorId { get; set; }
        public EnumCargo Cargo { get; set; }
    }
}
