using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoRHWeb.Migrations
{
    public partial class updateSolicitacaoRemoveItens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensSolicitacao_Solicitacoes_SolicitacaoId",
                table: "ItensSolicitacao");

            migrationBuilder.DropIndex(
                name: "IX_ItensSolicitacao_SolicitacaoId",
                table: "ItensSolicitacao");

            migrationBuilder.DropColumn(
                name: "SolicitacaoId",
                table: "ItensSolicitacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SolicitacaoId",
                table: "ItensSolicitacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItensSolicitacao_SolicitacaoId",
                table: "ItensSolicitacao",
                column: "SolicitacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensSolicitacao_Solicitacoes_SolicitacaoId",
                table: "ItensSolicitacao",
                column: "SolicitacaoId",
                principalTable: "Solicitacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
