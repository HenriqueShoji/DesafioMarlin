using DesafioMarlin.Domain.Entities.Dtos;
using DesafioMarlin.Domain.Entities.Models;
using DesafioMarlin.Services.TurmaServices;
using Microsoft.AspNetCore.Mvc;

namespace DesafioMarlin.Controllers
{
    [ApiController]
    [Route("turma")]
    public class TurmaController : Controller
    {
        public TurmaService _turmaService { get; set; }
        public TurmaController(TurmaService turmaService) 
        {
            _turmaService = turmaService;
        }

        [HttpGet("consulta")]
        public async Task<IActionResult> ListarTurmas()
        {
            try
            {
                List<Turma> turmas = await _turmaService.ListarTurmas();

                return Ok(turmas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("consulta/{id}")]
        public async Task<IActionResult> BuscarTurmaId(int id)
        {
            try
            {
                Turma turma = await _turmaService.BuscarPorId(id);

                return Ok(turma);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("alterar")]
        public async Task<IActionResult> AlterarTurma(AtualizarTurmaDto inputDto)
        {
            try
            {
                await _turmaService.AlterarTurma(inputDto);

                return Ok("Turma atualizada com sucesso!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deletar/{id}")]
        public async Task<IActionResult> ExcluirTurma(int id)
        {
            try
            {
                await _turmaService.ExcluirTurma(id);

                return Ok("Turma excluida com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarTurma(CadastrarTurmaDto inputDto)
        {
            try
            {
                await _turmaService.CadastrarTurma(inputDto);

                return Ok("Turma cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("matricular-aluno")]
        public async Task<IActionResult> MatricularAluno(MatricularAlunoDto inputDto)
        {
            try
            {
                await _turmaService.MatricularAlunoTurma(inputDto);

                return Ok("Aluno matriculado com sucesso!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpDelete("desmatricular-aluno")]
        public async Task<IActionResult> DesmatricularAluno(DesmatricularAluno inputDto)
        {
            try
            {
                await _turmaService.DesmatricularAluno(inputDto);

                return Ok("Aluno desmatriculado da turma com sucesso!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("alunos-matriculados")]
        public async Task<IActionResult> BuscarAlunosMatriculados()
        {
            try
            {
                List<AlunosTurma> matriculados = await _turmaService.BuscarAlunosMatriculados();

                return Ok(matriculados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("alunos-matriculados/{numeroTurma}")]
        public async Task<IActionResult> BuscarMatriculadosTurmaId(int numeroTurma)
        {
            try
            {
                List<AlunosTurma> turmaMatriculados = await _turmaService.BuscarMatriculadosTurmaId(numeroTurma);

                return Ok(turmaMatriculados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
