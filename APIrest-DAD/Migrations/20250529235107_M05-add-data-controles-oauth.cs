using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIrest_DAD.Migrations
{
    /// <inheritdoc />
    public partial class M05adddatacontrolesoauth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "OAUTH_TOKEN",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "expires_at",
                table: "OAUTH_TOKEN",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "OAUTH_TOKEN",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "OAUTH_TOKEN");

            migrationBuilder.DropColumn(
                name: "expires_at",
                table: "OAUTH_TOKEN");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "OAUTH_TOKEN");
        }
    }
}
