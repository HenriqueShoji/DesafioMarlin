using DesafioMarlin.Domain.Entities.Dtos;

namespace DesafioMarlin.Services.AlunoServices
{
    public class AlunoService
    {
        public IConfiguration _configuration { get; set; }

        public AlunoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task RegistrarAluno(RegistrarAlunoDto inputDto)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}
