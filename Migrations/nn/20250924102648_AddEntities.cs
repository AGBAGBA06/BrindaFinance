using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FacturationApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactureDetails");

            migrationBuilder.DropColumn(
                name: "MontantTotal",
                table: "Factures");

            migrationBuilder.RenameColumn(
                name: "DateEmission",
                table: "Factures",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Produits",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "Clients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "LignesFacture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FactureId = table.Column<int>(type: "integer", nullable: false),
                    ProduitId = table.Column<int>(type: "integer", nullable: false),
                    Quantite = table.Column<int>(type: "integer", nullable: false),
                    PrixTotal = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LignesFacture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LignesFacture_Factures_FactureId",
                        column: x => x.FactureId,
                        principalTable: "Factures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LignesFacture_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LignesFacture_FactureId",
                table: "LignesFacture",
                column: "FactureId");

            migrationBuilder.CreateIndex(
                name: "IX_LignesFacture_ProduitId",
                table: "LignesFacture",
                column: "ProduitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LignesFacture");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Produits");

            migrationBuilder.DropColumn(
                name: "Adresse",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Factures",
                newName: "DateEmission");

            migrationBuilder.AddColumn<decimal>(
                name: "MontantTotal",
                table: "Factures",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "FactureDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FactureId = table.Column<int>(type: "integer", nullable: false),
                    ProduitId = table.Column<int>(type: "integer", nullable: false),
                    PrixUnitaire = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantite = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactureDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactureDetails_Factures_FactureId",
                        column: x => x.FactureId,
                        principalTable: "Factures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactureDetails_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FactureDetails_FactureId",
                table: "FactureDetails",
                column: "FactureId");

            migrationBuilder.CreateIndex(
                name: "IX_FactureDetails_ProduitId",
                table: "FactureDetails",
                column: "ProduitId");
        }
    }
}
