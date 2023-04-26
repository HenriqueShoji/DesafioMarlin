using DesafioMarlin.Domain.Entities.Dtos;
using DesafioMarlin.Services.AlunoServices;
using Microsoft.AspNetCore.Mvc;

namespace DesafioMarlin.Controllers
{
    public class AlunoController : Controller
    {
        public AlunoService _alunoService { get; set; }
        public AlunoController(AlunoService alunoService) 
        {
            _alunoService = alunoService;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarAlunoDto inputDto)
        {
            try
            {
                await _alunoService.RegistrarAluno(inputDto);

                return Ok("Aluno Registrado com sucesso!"); 
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }


    }
}
