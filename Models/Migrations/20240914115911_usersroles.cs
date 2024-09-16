using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class usersroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
