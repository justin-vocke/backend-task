using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTask.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentAndAdminRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0099d65e-4638-4c58-86e4-5c96aec8c66b", null, "Student", "STUDENT" },
                    { "dd8d6ddf-7d1a-4c1f-85ad-2589b376acf4", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0099d65e-4638-4c58-86e4-5c96aec8c66b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd8d6ddf-7d1a-4c1f-85ad-2589b376acf4");
        }
    }
}
