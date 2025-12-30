using Microsoft.AspNetCore.Mvc;
using OrgCore.Application.DTOs.Auth;
using OrgCore.Application.Interfaces;

namespace OrgCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    { 
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("gerar-token")]
        public async Task<IActionResult> GerarTokenAcesso([FromBody] AuthDto dto)
        {
            var resultado = await _authService.GerarTokenAcesso(dto);
            if (!(resultado == string.Empty))
            {
                return Ok(new { Mensagem = "Token de acesso gerado com sucesso!" });
            }
            return BadRequest(new { Mensagem = "Falha ao gerar token de acesso." });
        }
    }
}
