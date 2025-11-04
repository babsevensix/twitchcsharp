using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaIndirizzi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndirizzoEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Default = table.Column<bool>(type: "boolean", nullable: false),
                    NomeCitta = table.Column<string>(type: "text", nullable: false),
                    Via = table.Column<string>(type: "text", nullable: false),
                    Cap = table.Column<string>(type: "text", nullable: false),
                    PersonaEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndirizzoEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndirizzoEntity_Persone_PersonaEntityId",
                        column: x => x.PersonaEntityId,
                        principalTable: "Persone",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndirizzoEntity_PersonaEntityId",
                table: "IndirizzoEntity",
                column: "PersonaEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndirizzoEntity");
        }
    }
}
