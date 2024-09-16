using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class itemsusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f94e39f-3312-457d-95a3-eec0c6415c41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1840603-7941-4979-a96a-68c353c8020a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e12abe15-9513-49a9-8ac6-3dc8964cb512");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6fa5323c-6563-4265-88d1-1fe0bac97c67", "cce2d836-dbc0-4ffb-8897-9d36e02654ac", "Category User", "category user" },
                    { "deef3121-de77-4a5a-9a24-c1309a7432a6", "d19eb7d8-9e31-4a92-b418-eef9ee101acc", "Admin", "admin" },
                    { "f114d4be-5b3f-4f46-a969-46b4e4c4efa9", "a730add1-da9a-43f8-a057-e8dc69d88a72", "Products User", "products user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fa5323c-6563-4265-88d1-1fe0bac97c67");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "deef3121-de77-4a5a-9a24-c1309a7432a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f114d4be-5b3f-4f46-a969-46b4e4c4efa9");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Items");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9f94e39f-3312-457d-95a3-eec0c6415c41", "cf50875f-9f6e-45c1-bfe9-2e4b38841bcb", "Products User", "products user" },
                    { "d1840603-7941-4979-a96a-68c353c8020a", "735d9b8d-69a6-4095-a7aa-8d12f2f5f460", "Admin", "admin" },
                    { "e12abe15-9513-49a9-8ac6-3dc8964cb512", "6c0b922f-f95a-480a-af0e-e9190031d583", "Category User", "category user" }
                });
        }
    }
}
