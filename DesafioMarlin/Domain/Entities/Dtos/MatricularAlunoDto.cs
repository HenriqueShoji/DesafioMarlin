using System.ComponentModel.DataAnnotations;

namespace DesafioMarlin.Domain.Entities.Dtos
{
    public class MatricularAlunoDto
    {
        public int Numero { get; set; }
        public int Ano_Letivo { get; set; }
        [MaxLength(11)]
        public string Cpf { get; set; }
        public string Nome_Aluno { get; set; }
    }
}
