using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeEnergyApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUtilityProvidersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Disable foreign key constraints temporarily
            migrationBuilder.Sql("PRAGMA foreign_keys = OFF;", suppressTransaction: true);
            migrationBuilder.CreateTable(
                name: "UtilityProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProvidedUtilities = table.Column<string>(type: "TEXT", nullable: false),
                    HomeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilityProviders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UtilityProviders_Homes_HomeId",
                        column: x => x.HomeId,
                        principalTable: "Homes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UtilityProviders_HomeId",
                table: "UtilityProviders",
                column: "HomeId");
            // Re-enable foreign key constraints
            migrationBuilder.Sql("PRAGMA foreign_keys = ON;", suppressTransaction: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UtilityProviders");
        }
    }
}
