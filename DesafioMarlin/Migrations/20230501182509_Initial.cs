using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioMarlin.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DesafioMarlin");

            migrationBuilder.CreateTable(
                name: "tbAluno",
                schema: "DesafioMarlin",
                columns: table => new
                {
                    id_aluno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    email = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id_aluno", x => x.id_aluno);
                });

            migrationBuilder.CreateTable(
                name: "tbAlunosTurma",
                schema: "DesafioMarlin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_turma = table.Column<int>(type: "int", nullable: false),
                    ano_letivo = table.Column<int>(type: "int", nullable: false),
                    cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nome_aluno = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbTurma",
                schema: "DesafioMarlin",
                columns: table => new
                {
                    id_turma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero = table.Column<int>(type: "int", nullable: false),
                    ano_letivo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id_turma", x => x.id_turma);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbAluno",
                schema: "DesafioMarlin");

            migrationBuilder.DropTable(
                name: "tbAlunosTurma",
                schema: "DesafioMarlin");

            migrationBuilder.DropTable(
                name: "tbTurma",
                schema: "DesafioMarlin");
        }
    }
}
