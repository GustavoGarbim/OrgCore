using Microsoft.AspNetCore.Mvc;
using OrgCore.Application.DTOs.Pessoa;
using OrgCore.Application.Interfaces;

namespace OrgCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : Controller
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] CriarPessoaDto dto)
        {
            var pessoa = await _pessoaService.RegistrarPessoa(dto);
            return Ok(new { Pessoa = pessoa, Mensagem = "Pessoa Registrada com sucesso!" });
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPessoaId([FromQuery] Guid id)
        {
            var pessoa = await _pessoaService.ObterPessoaPorId(id);
            return Ok(pessoa);
        }
    }
}
