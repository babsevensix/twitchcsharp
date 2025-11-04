using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaCitta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cap",
                table: "IndirizzoEntity");

            migrationBuilder.DropColumn(
                name: "NomeCitta",
                table: "IndirizzoEntity");

            migrationBuilder.AddColumn<int>(
                name: "LinkCittaId",
                table: "IndirizzoEntity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Citta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Cap = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citta", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndirizzoEntity_LinkCittaId",
                table: "IndirizzoEntity",
                column: "LinkCittaId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndirizzoEntity_Citta_LinkCittaId",
                table: "IndirizzoEntity",
                column: "LinkCittaId",
                principalTable: "Citta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndirizzoEntity_Citta_LinkCittaId",
                table: "IndirizzoEntity");

            migrationBuilder.DropTable(
                name: "Citta");

            migrationBuilder.DropIndex(
                name: "IX_IndirizzoEntity_LinkCittaId",
                table: "IndirizzoEntity");

            migrationBuilder.DropColumn(
                name: "LinkCittaId",
                table: "IndirizzoEntity");

            migrationBuilder.AddColumn<string>(
                name: "Cap",
                table: "IndirizzoEntity",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomeCitta",
                table: "IndirizzoEntity",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
