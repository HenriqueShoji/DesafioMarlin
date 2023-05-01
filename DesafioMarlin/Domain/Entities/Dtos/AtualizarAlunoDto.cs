using System.ComponentModel.DataAnnotations;

namespace DesafioMarlin.Domain.Entities.Dtos
{
    public class AtualizarAlunoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [MaxLength(11)]
        public string Cpf { get; set; }
        public string Email { get; set; }
    }
}
