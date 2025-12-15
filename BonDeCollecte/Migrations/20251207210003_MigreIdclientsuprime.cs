using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonDeCollecte.Migrations
{
    /// <inheritdoc />
    public partial class MigreIdclientsuprime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdClient",
                table: "BTCollectes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdClient",
                table: "BTCollectes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
