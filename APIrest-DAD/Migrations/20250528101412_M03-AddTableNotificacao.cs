using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIrest_DAD.Migrations
{
    /// <inheritdoc />
    public partial class M03AddTableNotificacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SERV_NOTIFICACAO",
                columns: table => new
                {
                    codigoNotificacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigoUsuario = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataNotificacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    dataPreNotificacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERV_NOTIFICACAO", x => x.codigoNotificacao);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SERV_NOTIFICACAO");
        }
    }
}
