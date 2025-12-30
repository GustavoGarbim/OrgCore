using Microsoft.AspNetCore.Mvc;
using OrgCore.Application.DTOs.Forms;
using OrgCore.Application.Interfaces;

namespace OrgCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormulariosController : ControllerBase
    {
        private readonly IFormularioService _formularioService;
        private readonly IAvaliacaoService _avaliacaoService;

        public FormulariosController(IFormularioService formularioService, IAvaliacaoService avaliacaoService)
        {
            _formularioService = formularioService;
            _avaliacaoService = avaliacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarFormularioDto dto)
        {
            var id = await _formularioService.CriarTemplate(dto);
            return Ok(new { Id = id, Mensagem = "Formulário criado com sucesso!" });
        }

        [HttpGet]
        public async Task<IActionResult> ObterPorEmpresa([FromQuery] Guid empresaId)
        {
            var formularios = await _formularioService.PegarFormulariosPorEmpresaAsync(empresaId);
            return Ok(formularios);
        }

        [HttpPost("responder")]
        public async Task<IActionResult> Responder([FromBody] RealizarAvaliacaoDto dto)
        {
            try
            {
                var id = await _avaliacaoService.EnviarResposta(dto);
                return Ok(new { Id = id, Mensagem = "Avaliação enviada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(Guid id)
        {
            var dto = await _avaliacaoService.ObterRelatorio(id);
            return Ok(dto);
        }
    }
}