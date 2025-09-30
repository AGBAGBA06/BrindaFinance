using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacturationApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovePrixTotalFromLignes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrixTotal",
                table: "LignesFacture");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrixTotal",
                table: "LignesFacture",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
