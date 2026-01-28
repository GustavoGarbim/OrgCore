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
            try 
            {
                var pessoa = await _pessoaService.RegistrarPessoa(dto);
                var resposta = new 
                { 
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    Email = pessoa.EmailPessoal,
                    Mensagem = "Pessoa Registrada com sucesso!" 
                };

                return CreatedAtAction(nameof(BuscarPessoaId), new { id = pessoa.Id }, resposta);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPessoaId([FromQuery] Guid id)
        {
            var pessoa = await _pessoaService.ObterPessoaPorId(id);
            return Ok(pessoa);
        }
    }
}
