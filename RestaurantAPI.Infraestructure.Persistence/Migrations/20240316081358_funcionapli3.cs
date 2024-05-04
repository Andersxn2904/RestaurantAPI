using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantAPI.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class funcionapli3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Tables");

            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TableStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableStatus", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "TableStatus",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Available" },
                    { 2, "In process of Attention" },
                    { 3, "Attended" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_StatusID",
                table: "Tables",
                column: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_TableStatus_StatusID",
                table: "Tables",
                column: "StatusID",
                principalTable: "TableStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_TableStatus_StatusID",
                table: "Tables");

            migrationBuilder.DropTable(
                name: "TableStatus");

            migrationBuilder.DropIndex(
                name: "IX_Tables_StatusID",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "Tables");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
