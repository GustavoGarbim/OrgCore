using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Application.DTOs
{
    public class VincularColaboradorDto
    {
        public Guid EmpresaId { get; set; }
        public Guid PessoaId { get; set; }
    }
}
