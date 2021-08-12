using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StandByClientes.Migrations
{
    public partial class migration_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Razao_Social = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_Fundacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Capital = table.Column<double>(type: "float", nullable: false),
                    Quarentena = table.Column<bool>(type: "bit", nullable: false),
                    Status_Cliente = table.Column<bool>(type: "bit", nullable: false),
                    Classificacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciente", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ciente");
        }
    }
}
