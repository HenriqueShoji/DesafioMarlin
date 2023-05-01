namespace DesafioMarlin.Domain.Entities.Models
{
    public class AlunosTurma
    {
        public int Id { get; set; }
        public int Numero_Turma { get; set; }
        public int Ano_Letivo { get; set; }
        public string Cpf { get; set; }
        public string Nome_Aluno { get; set; }
    }
}
