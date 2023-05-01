using DesafioMarlin.Domain.Entities.Dtos;
using DesafioMarlin.Middleware.Exceptions;
using DesafioMarlin.Services.AlunoServices;
using Microsoft.AspNetCore.Mvc;

namespace DesafioMarlin.Controllers
{
    [ApiController]
    [Route("aluno")]
    public class AlunoController : Controller
    {
        public AlunoService _alunoService { get; set; }
        public AlunoController(AlunoService alunoService) 
        {
            _alunoService = alunoService;
        }

        [HttpGet("consulta")]
        public async Task<IActionResult> ListarAlunos()
        {
            try
            {
                var alunos = await _alunoService.ListarAlunos();

                return Ok(alunos);
            }
            catch(NenhumAlunoException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("consulta/{id}")]
        public async Task<IActionResult> ConsultarAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.ConsultarAlunoId(id);

                return Ok(aluno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> RegistrarAluno(CadastrarAlunoDto inputDto)
        {
            try
            {
                await _alunoService.RegistrarAluno(inputDto);

                return Ok("Aluno Registrado com sucesso!"); 
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("alterar")]
        public async Task<IActionResult> AlterarAluno(AtualizarAlunoDto inputDto)
        {
            try
            {
                await _alunoService.AtualizarAluno(inputDto);

                return Ok("Aluno alterado com sucesso!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> ExcluirAluno(int id)
        {
            try
            {
                await _alunoService.ExcluirAluno(id);

                return Ok("Aluno excluido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
