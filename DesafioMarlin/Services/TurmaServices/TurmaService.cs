using DesafioMarlin.Domain.Entities.Dtos;
using DesafioMarlin.Domain.Entities.Models;
using DesafioMarlin.Middleware.Exceptions;
using DesafioMarlin.Repositories;

namespace DesafioMarlin.Services.TurmaServices
{
    public class TurmaService
    {
        private VerificacaoService _verificacaoService { get; set; }
        public TurmaService(VerificacaoService verificacaoService)
        {
            _verificacaoService = verificacaoService;
        }

        public async Task<List<Turma>> ListarTurmas()
        {
            try
            {
                using (Context db = new Context())
                {
                    List<Turma> turmas = db.Turma.ToList();
                    if (turmas.Count == 0)
                        throw new NenhumaTurmaException("Nenhuma turma cadastrada!");

                    return turmas;
                }
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public async Task<Turma> BuscarPorId(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("Id de busca não pode ser menor ou igual a zero!");

                using (Context db = new Context())
                {
                    Turma turma = db.Turma.FirstOrDefault(x => x.Id_Turma == id);

                    if (turma == null)
                        throw new Exception("Nenhuma turma encontrada com este Id!");

                    return turma;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AlterarTurma(AtualizarTurmaDto inputDto)
        {
            try
            {
                using (Context db = new Context())
                {
                    Turma turma = db.Turma.FirstOrDefault(x => x.Id_Turma == inputDto.Id);

                    if (inputDto.Numero != turma.Numero && inputDto.Numero > 0)
                        turma.Numero = inputDto.Numero;
                    else
                        throw new Exception("Numero da turma não pode ser igual o número anterior e não pode ser menor que 0!");

                    if (inputDto.Ano_Letivo != turma.Ano_Letivo && inputDto.Ano_Letivo > 1999)
                        turma.Ano_Letivo = inputDto.Ano_Letivo;
                    else
                        throw new Exception("Ano letivo deve ser maior que 1999!");

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CadastrarTurma(CadastrarTurmaDto inputDto)
        {
            try
            {
                if (inputDto.Numero <= 0)
                    throw new Exception("Numero da turma não pode ser menor que 0!");

                if (inputDto.Ano_Letivo <= 1999)
                    throw new Exception("Ano letivo deve ser maior que 1999!");

                using (Context db = new Context())
                {
                    Turma turma = new Turma
                    {
                        Numero = inputDto.Numero,
                        Ano_Letivo = inputDto.Ano_Letivo
                    };

                    db.Turma.Add(turma);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task ExcluirTurma(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("Id da turma não pode ser menor que 0!");

                using (Context db = new Context())
                {
                    Turma turma = db.Turma.FirstOrDefault(x => x.Id_Turma == id);

                    if (turma == null)
                        throw new NenhumaTurmaException("Nenhuma turma cadastrada com esse Id!");

                    if (db.AlunosTurma.Any(x => x.Numero_Turma == turma.Numero))
                        throw new Exception("Existem alunos matriculados nesta turma, favor remover os alunos primeiro!");

                    db.Turma.Remove(turma);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task MatricularAlunoTurma(MatricularAlunoDto inputDto)
        {
            try
            {
                if (inputDto.Numero <= 0)
                    throw new Exception("Numero da turma não pode ser menor que 0!");

                if (inputDto.Ano_Letivo <= 1999)
                    throw new Exception("Ano letivo deve ser maior que 1999!");

                if (string.IsNullOrWhiteSpace(inputDto.Nome_Aluno))
                    throw new Exception("Nome não pode ser nulo ou vazio!");

                if (!_verificacaoService.VerificaCpf(inputDto.Cpf))
                    throw new CpfInvalidoException("O cpf inserido é invalido!");

                using (Context db = new Context())
                {
                    int alunosCadastrados = db.AlunosTurma.Where(x => x.Numero_Turma == inputDto.Numero).Count();
                    if (alunosCadastrados >= 5)
                        throw new Exception("Máximo de alunos matriculados na turma atingido!");

                    if (!db.Aluno.Any(x => x.Cpf == inputDto.Cpf))
                        throw new Exception("Favor fornecer o CPF de um aluno já cadastrado!");

                    AlunosTurma alunosTurma = new AlunosTurma
                    {
                        Numero_Turma = inputDto.Numero,
                        Ano_Letivo = inputDto.Ano_Letivo,
                        Cpf = inputDto.Cpf,
                        Nome_Aluno = inputDto.Nome_Aluno
                    };

                    db.AlunosTurma.Add(alunosTurma);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task DesmatricularAluno(DesmatricularAluno inputDto)
        {
            try
            {
                if (inputDto.Numero <= 0)
                    throw new Exception("Numero da turma não pode ser menor que 0!");

                if (inputDto.Ano_Letivo <= 1999)
                    throw new Exception("Ano letivo deve ser maior que 1999!");

                if (!_verificacaoService.VerificaCpf(inputDto.Cpf))
                    throw new CpfInvalidoException("O cpf inserido é invalido!");

                using (Context db = new Context())
                {
                    if (!db.AlunosTurma.Any(x => x.Cpf == inputDto.Cpf))
                        throw new Exception("Nenhum aluno foi cadastrado com o CPF inserido!");

                    AlunosTurma alunosTurma = db.AlunosTurma.FirstOrDefault(x => x.Numero_Turma == inputDto.Numero && x.Ano_Letivo == inputDto.Ano_Letivo 
                        && x.Cpf == inputDto.Cpf);

                    db.AlunosTurma.Remove(alunosTurma);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<AlunosTurma>> BuscarAlunosMatriculados()
        {
            try
            {
                using (Context db = new Context())
                {
                    List<AlunosTurma> matriculados = db.AlunosTurma.ToList();
                    if (matriculados.Count == 0)
                        throw new NenhumaTurmaException("Nenhuma turma cadastrada!");

                    return matriculados;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<AlunosTurma>> BuscarMatriculadosTurmaId(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("Id de busca não pode ser menor ou igual a zero!");

                using (Context db = new Context())
                {
                    List<AlunosTurma> turmaMatriculados = db.AlunosTurma.Where(x => x.Numero_Turma == id).ToList();

                    if (turmaMatriculados == null)
                        throw new Exception("Nenhuma turma encontrada com este Id!");

                    return turmaMatriculados;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
