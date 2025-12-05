using Microsoft.AspNetCore.Mvc;
using OrgCore.Application.DTOs;
using OrgCore.Application.Interfaces;

namespace OrgCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorService _colaboradorService;

        public ColaboradorController(IColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }

        [HttpPost]
        public async Task<IActionResult> Vincular([FromBody] VincularColaboradorDto dto)
        {
            await _colaboradorService.VincularColaborador(dto);
            return Ok($"Colaborador {dto.PessoaId} vinculado com sucesso a empresa {dto.EmpresaId}!");
        }
    }
}
