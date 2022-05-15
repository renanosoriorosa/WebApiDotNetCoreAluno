using Microsoft.EntityFrameworkCore.Migrations;

namespace AlunoApi.Migrations
{
    public partial class popular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Aluno",
                columns: new[] { "Id", "Email", "Idade", "Nome" },
                values: new object[] { 1, "Renan@email.com", 28, "Renan Osório" });

            migrationBuilder.InsertData(
                table: "Aluno",
                columns: new[] { "Id", "Email", "Idade", "Nome" },
                values: new object[] { 2, "Wagner@email.com", 30, "Wagner Rodrigues" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Aluno",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Aluno",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
