using Microsoft.AspNetCore.Mvc;
using OrgCore.Application.DTOs.Forms;
using OrgCore.Application.Interfaces;

namespace OrgCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormulariosController : ControllerBase
    {
        private readonly IFormularioService _service;

        public FormulariosController(IFormularioService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarFormularioDto dto)
        {
            var id = await _service.CriarTemplate(dto);
            return Ok(new { Id = id, Mensagem = "Formulário criado com sucesso!" });
        }

        [HttpGet]
        public async Task<IActionResult> ObterPorEmpresa([FromQuery] Guid empresaId)
        {
            var formularios = await _service.PegarFormulariosPorEmpresaAsync(empresaId);
            return Ok(formularios);
        }
    }
}