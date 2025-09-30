using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacturationApp.Migrations
{
    /// <inheritdoc />
    public partial class FactureTvaRemise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Remise",
                table: "Factures",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tva",
                table: "Factures",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remise",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "Tva",
                table: "Factures");
        }
    }
}
