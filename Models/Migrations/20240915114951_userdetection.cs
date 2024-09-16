using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class userdetection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f4ed5c0-36cb-4fc0-bc8f-9533038fd963");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b572af24-7a66-4ab0-96be-7ec00f2251e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff3b538d-8ff7-4a6e-9889-e36b28081a09");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9f94e39f-3312-457d-95a3-eec0c6415c41", "cf50875f-9f6e-45c1-bfe9-2e4b38841bcb", "Products User", "products user" },
                    { "d1840603-7941-4979-a96a-68c353c8020a", "735d9b8d-69a6-4095-a7aa-8d12f2f5f460", "Admin", "admin" },
                    { "e12abe15-9513-49a9-8ac6-3dc8964cb512", "6c0b922f-f95a-480a-af0e-e9190031d583", "Category User", "category user" }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedBy",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedBy",
                value: null);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedBy",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5f4ed5c0-36cb-4fc0-bc8f-9533038fd963", "136e5bc6-a55b-41ae-bcbb-73c0f54fafbe", "Products User", "products user" },
                    { "b572af24-7a66-4ab0-96be-7ec00f2251e0", "a2b7595d-4b1c-4b2a-8bf8-d4f540fa0f6f", "Admin", "admin" },
                    { "ff3b538d-8ff7-4a6e-9889-e36b28081a09", "20475fde-0316-4f48-b6a6-b2a28abf2028", "Category User", "category user" }
                });
        }
    }
}
