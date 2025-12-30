using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Application.DTOs.Pessoa
{
    public class PessoaDTO
    {
        public Guid UsuarioId { get; set; }
    }

    public class CriarPessoaDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string EmailPessoal { get; set; }
    }
}
