using DesafioMarlin.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioMarlin.Repositories
{
    public class Context : DbContext
    {
        public Context() { }

        public Context(DbContextOptions<Context> opts) : base(opts) { }

        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Turma> Turma { get; set; }

        private void ConfigurarAluno(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.ToTable("tbAluno");
                entity.HasKey(x => x.Id).HasName("id");
                entity.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(50).IsRequired();
                entity.Property(x => x.Cpf).HasColumnName("cpf").HasMaxLength(11).IsRequired();
                entity.Property(x => x.Email).HasColumnName("email").HasMaxLength(55).IsRequired();
            });
        }
    }
}
