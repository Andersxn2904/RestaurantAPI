using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AnotherChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersDishes",
                table: "OrdersDishes");

            migrationBuilder.DropIndex(
                name: "IX_OrdersDishes_OrderID",
                table: "OrdersDishes");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersDishes",
                table: "OrdersDishes",
                columns: new[] { "OrderID", "DishID" });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDishes_DishID",
                table: "OrdersDishes",
                column: "DishID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersDishes",
                table: "OrdersDishes");

            migrationBuilder.DropIndex(
                name: "IX_OrdersDishes_DishID",
                table: "OrdersDishes");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersDishes",
                table: "OrdersDishes",
                columns: new[] { "DishID", "OrderID" });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDishes_OrderID",
                table: "OrdersDishes",
                column: "OrderID");
        }
    }
}
