using OrgCore.Application.DTOs.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Application.Interfaces
{
    public interface IAvaliacaoService
    {
        Task<Guid> EnviarResposta(RealizarAvaliacaoDto dto);

        Task<VisualizarAvaliacaoDto> ObterRelatorio(Guid id);
    }
}
