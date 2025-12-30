using Microsoft.AspNetCore.Mvc;
using OrgCore.Application.DTOs.Empresa.Colaborador;
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
        public async Task<IActionResult> Vincular([FromBody] CadastrarColaboradorDTO dto)
        {
            await _colaboradorService.VincularColaborador(dto);
            return Ok($"Pessoa: {dto.PessoaId} vinculado com sucesso a Empresa: {dto.EmpresaId}");
        }

        [HttpGet("{empresaId}")]
        public async Task<IActionResult> ObterColaboradoresPorEmpresaId(Guid empresaId)
        {
            var colaboradores = await _colaboradorService.ObterColaboradoresPorEmpresaId(empresaId);
            return Ok(colaboradores);
        }

        [HttpGet("colaborador/{colaboradorId}")]
        public async Task<IActionResult> ObterColaboradorPorId(Guid colaboradorId)
        {
            var colaborador = await _colaboradorService.ObterColaboradorPorId(colaboradorId);
            if (colaborador == null)
            {
                return NotFound($"Colaborador com ID: {colaboradorId} não encontrado.");
            }
            return Ok(colaborador);
        }
    }
}
