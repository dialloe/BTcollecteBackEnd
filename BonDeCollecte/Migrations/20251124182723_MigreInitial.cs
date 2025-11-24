using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonDeCollecte.Migrations
{
    /// <inheritdoc />
    public partial class MigreInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BTCollectes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    NbreFuts20L = table.Column<double>(type: "float", nullable: false),
                    NbreFuts60L = table.Column<double>(type: "float", nullable: false),
                    CapaciteEnLitre = table.Column<double>(type: "float", nullable: false),
                    PoidsTotal = table.Column<double>(type: "float", nullable: false),
                    Montant = table.Column<double>(type: "float", nullable: false),
                    Paye = table.Column<bool>(type: "bit", nullable: false),
                    Validation = table.Column<bool>(type: "bit", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BTCollectes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BTCollectes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BTCollectes_ClientId",
                table: "BTCollectes",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BTCollectes");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
