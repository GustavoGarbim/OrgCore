using Microsoft.AspNetCore.Mvc;
using OrgCore.Application.DTOs;
using OrgCore.Application.Interfaces;
using OrgCore.Domain.Evaluation.Enums;

namespace OrgCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncaotesteController : ControllerBase
    {
        private readonly IFuncaoTeste _funcaoTesteCargoService;

        public FuncaotesteController(IFuncaoTeste funcaoTesteCargoService)
        {
            _funcaoTesteCargoService = funcaoTesteCargoService;
        }

        [HttpPost]
        public Task<EnumCargo> Teste([FromBody] FuncaoTesteDTO dto)
        {
            var esp = _funcaoTesteCargoService.FuncaoTesteCargo(dto);
            return esp;
        }
    }
}
