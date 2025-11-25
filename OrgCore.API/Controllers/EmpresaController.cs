using Microsoft.AspNetCore.Mvc;
using OrgCore.Application.DTOs;
using OrgCore.Application.Interfaces;

namespace OrgCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] NovaEmpresaDto dto)
        {
            var id = await _empresaService.RegistrarEmpresa(dto);
            return Ok(new { Id = id, Mensagem = "Empresa criada com sucesso!" });
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            var empresas = await _empresaService.ObterTodasEmpresas();
            return Ok(empresas);
        }
    }
}
