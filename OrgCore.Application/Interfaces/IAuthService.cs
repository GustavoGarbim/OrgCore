using OrgCore.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> GerarTokenAcesso(AuthDto dto);
    }
}
