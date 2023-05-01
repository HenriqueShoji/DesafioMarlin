using DesafioMarlin.Domain.Entities.Dtos;
using DesafioMarlin.Domain.Entities.Models;
using DesafioMarlin.Middleware.Exceptions;
using DesafioMarlin.Repositories;
using DesafioMarlin.Services.TurmaServices;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DesafioMarlin.Services.AlunoServices
{
    public class AlunoService
    {
        private VerificacaoService _verificacaoService { get; set; }
        private TurmaService _turmaService { get; set; }

        public AlunoService(VerificacaoService verificacaoService, TurmaService turmaService)
        {
            _verificacaoService = verificacaoService;
            _turmaService = turmaService;
        }

        public async Task<List<Aluno>> ListarAlunos()
        {
            try
            {
                using (Context db = new Context())
                {
                    List<Aluno> alunos = db.Aluno.ToList();
                    if (alunos.Count == 0)
                        throw new NenhumAlunoException("Nenhum aluno registrado!");

                    return alunos;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Aluno> ConsultarAlunoId(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("Id de busca não pode ser menor ou igual a zero!");

                using (Context db = new Context())
                {
                    var aluno = db.Aluno.FirstOrDefault(x => x.Id_Aluno == id);

                    if (aluno == null)
                        throw new Exception("Nenhum aluno encontrado com este Id");

                    return aluno;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task RegistrarAluno(CadastrarAlunoDto inputDto)
        {
            try
            {
                if (!_verificacaoService.VerificaCpf(inputDto.Cpf))
                    throw new CpfInvalidoException("O cpf inserido é invalido!");
                if (!_verificacaoService.VerificaEmail(inputDto.Email))
                    throw new EmailInvalidoException("O email inserido é inválido!");
                using (Context db = new Context())
                {
                    if (db.Aluno.Any(x => x.Cpf == inputDto.Cpf))
                        throw new CpfCadastradoException("O cpf utilizado já esta cadastrado!");

                    if (db.Aluno.Any(x => x.Email == inputDto.Email))
                        throw new EmailCadastradoException("O email utilizado já esta cadastrado!");

                    if (!db.Turma.Any(x => x.Numero == inputDto.Numero_Turma && x.Ano_Letivo == inputDto.Ano_Letivo))
                        throw new Exception("Turma não encontrada, favor selecionar uma turma já cadastrada ou fazer o cadastro de uma nova!");

                    Aluno aluno = new Aluno
                    {
                        Nome = inputDto.Nome,
                        Cpf = inputDto.Cpf,
                        Email = inputDto.Email,
                    };

                    db.Aluno.Add(aluno);
                    db.SaveChanges();

                    MatricularAlunoDto matricularAluno = new MatricularAlunoDto
                    {
                        Ano_Letivo = inputDto.Ano_Letivo,
                        Numero = inputDto.Numero_Turma,
                        Nome_Aluno = inputDto.Nome,
                        Cpf = inputDto.Cpf
                    };

                    await _turmaService.MatricularAlunoTurma(matricularAluno);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AtualizarAluno(AtualizarAlunoDto inputDto)
        {
            try
            {
                using (Context db = new Context())
                {
                    var aluno = db.Aluno.FirstOrDefault(x => x.Id_Aluno == inputDto.Id);

                    if (!_verificacaoService.VerificaCpf(inputDto.Cpf))
                        aluno.Cpf = inputDto.Cpf;

                    if (!string.IsNullOrWhiteSpace(inputDto.Nome))
                        aluno.Nome = inputDto.Nome;

                    if (!string.IsNullOrWhiteSpace(inputDto.Email))
                        aluno.Email = inputDto.Email;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ExcluirAluno(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("Id de busca não pode ser menor ou igual a zero!");

                using (Context db = new Context())
                {
                    Aluno aluno = db.Aluno.FirstOrDefault(x => x.Id_Aluno == id);

                    if (db.AlunosTurma.Any(x => x.Cpf == aluno.Cpf))
                        throw new Exception("Aluno matriculado em uma turma, favor remover a matricula antes de excluir o aluno!");

                    db.Aluno.Remove(aluno);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
