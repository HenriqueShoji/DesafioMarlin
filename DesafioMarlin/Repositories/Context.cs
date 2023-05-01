using DesafioMarlin.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioMarlin.Repositories
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions<Context> opts) : base(opts) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=DesafioMarlin;Trusted_Connection=true;TrustServerCertificate=true;");
        }
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<AlunosTurma> AlunosTurma { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DesafioMarlin");

            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.ToTable("tbAluno");
                entity.HasKey(x => x.Id_Aluno).HasName("id_aluno");
                entity.Property(x => x.Id_Aluno).HasColumnName("id_aluno").ValueGeneratedOnAdd();
                entity.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(50).IsRequired();
                entity.Property(x => x.Cpf).HasColumnName("cpf").HasMaxLength(11).IsRequired();
                entity.Property(x => x.Email).HasColumnName("email").HasMaxLength(55).IsRequired();
            });

            modelBuilder.Entity<Turma>(entity =>
            {
                entity.ToTable("tbTurma");
                entity.HasKey(x => x.Id_Turma).HasName("id_turma");
                entity.Property(x => x.Id_Turma).HasColumnName("id_turma").ValueGeneratedOnAdd();
                entity.Property(x => x.Numero).HasColumnName("numero").IsRequired();
                entity.Property(x => x.Ano_Letivo).HasColumnName("ano_letivo").IsRequired();
            });

            modelBuilder.Entity<AlunosTurma>(entity =>
            {
                entity.ToTable("tbAlunosTurma");
                entity.HasKey(x => x.Id).HasName("id");
                entity.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(x => x.Numero_Turma).HasColumnName("numero_turma");
                entity.Property(x => x.Ano_Letivo).HasColumnName("ano_letivo");
                entity.Property(x => x.Nome_Aluno).HasColumnName("nome_aluno");
                entity.Property(x => x.Cpf).HasColumnName("cpf");
            });
        }
    }
}
