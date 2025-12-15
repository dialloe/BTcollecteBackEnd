using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonDeCollecte.Migrations
{
    /// <inheritdoc />
    public partial class updateBtCollecte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NbreFuts20L",
                table: "BTCollectes");

            migrationBuilder.RenameColumn(
                name: "NbreFuts60L",
                table: "BTCollectes",
                newName: "CapaciteEnLitreValide");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CapaciteEnLitreValide",
                table: "BTCollectes",
                newName: "NbreFuts60L");

            migrationBuilder.AddColumn<double>(
                name: "NbreFuts20L",
                table: "BTCollectes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
