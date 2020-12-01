using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoRHWeb.Migrations
{
    public partial class updateSolicitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Caixas_CaixaId",
                table: "Solicitacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Funcionarios_FuncionarioId",
                table: "Solicitacoes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitacoes_CaixaId",
                table: "Solicitacoes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitacoes_FuncionarioId",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "CaixaId",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Solicitacoes");

            migrationBuilder.AddColumn<string>(
                name: "CarrinhoId",
                table: "Solicitacoes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "Solicitacoes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarrinhoId",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "Solicitacoes");

            migrationBuilder.AddColumn<int>(
                name: "CaixaId",
                table: "Solicitacoes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId",
                table: "Solicitacoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_CaixaId",
                table: "Solicitacoes",
                column: "CaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_FuncionarioId",
                table: "Solicitacoes",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Caixas_CaixaId",
                table: "Solicitacoes",
                column: "CaixaId",
                principalTable: "Caixas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Funcionarios_FuncionarioId",
                table: "Solicitacoes",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
