using System.ComponentModel.DataAnnotations;

namespace DesafioMarlin.Domain.Entities.Dtos
{
    public class CadastrarAlunoDto
    {
        public string Nome { get; set; }
        [MaxLength(11)]
        public string Cpf { get; set; }
        public string Email { get; set; }
        public int Numero_Turma { get; set; }
        public int Ano_Letivo { get; set; }
    }
}
