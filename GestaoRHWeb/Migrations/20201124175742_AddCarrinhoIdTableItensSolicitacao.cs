using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoRHWeb.Migrations
{
    public partial class AddCarrinhoIdTableItensSolicitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarrinhoId",
                table: "ItensSolicitacao",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Custodia",
                table: "ItensSolicitacao",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Matricula",
                table: "ItensSolicitacao",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarrinhoId",
                table: "ItensSolicitacao");

            migrationBuilder.DropColumn(
                name: "Custodia",
                table: "ItensSolicitacao");

            migrationBuilder.DropColumn(
                name: "Matricula",
                table: "ItensSolicitacao");
        }
    }
}
