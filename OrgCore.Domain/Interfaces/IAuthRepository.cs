using OrgCore.Domain.Contexts.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgCore.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<Pessoa?> GerarTokenAcesso(string cpf);
    }
}
