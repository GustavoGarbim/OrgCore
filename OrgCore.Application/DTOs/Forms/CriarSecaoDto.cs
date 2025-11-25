using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Application.DTOs.Forms
{
    public class CriarSecaoDto
    {
        public string Titulo { get; set; }
        public int Ordem { get; set; }
        public List<CriarQuestaoDto> Questoes { get; set; } = new();
    }
}
