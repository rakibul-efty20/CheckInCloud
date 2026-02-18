using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CheckInCloud.Api.Migrations
{
    /// <inheritdoc />
    public partial class RoleConfigurationupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "71da116a-0e6e-4bfb-b9f7-6e4a85de7076", null, "User", "USER" },
                    { "8da8e3f1-8eec-41a5-a934-51e0c70ebeca", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71da116a-0e6e-4bfb-b9f7-6e4a85de7076");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8da8e3f1-8eec-41a5-a934-51e0c70ebeca");
        }
    }
}
