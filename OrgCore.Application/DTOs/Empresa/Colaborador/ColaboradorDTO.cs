using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Application.DTOs.Empresa.Colaborador
{
    public class ColaboradorDTO
    {
        public Guid ColaboradorId { get; set; }
        public Guid EmpresaId { get; set; }
        public string NomeEmpresa { get; set; }
        public Guid PessoaId { get; set; }
        public string NomePessoa { get; set; }
    }

    public class CadastrarColaboradorDTO
    {
        public Guid EmpresaId { get; set; }
        public Guid PessoaId { get; set; }
    }
}
