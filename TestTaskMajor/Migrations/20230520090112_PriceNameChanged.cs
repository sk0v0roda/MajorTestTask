using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTaskMajor.Migrations
{
    /// <inheritdoc />
    public partial class PriceNameChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Items",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Items",
                newName: "price");
        }
    }
}
