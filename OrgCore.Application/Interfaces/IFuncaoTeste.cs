using OrgCore.Application.DTOs;
using OrgCore.Domain.Evaluation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Application.Interfaces
{
    public interface IFuncaoTeste
    {
        Task<EnumCargo> FuncaoTesteCargo(FuncaoTesteDTO dto);
    }
}
