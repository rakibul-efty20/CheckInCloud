using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckInCloud.Api.Migrations
{
    /// <inheritdoc />
    public partial class changestheCountryandHotelsname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hotels_countries_CountryId",
                table: "hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hotels",
                table: "hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_countries",
                table: "countries");

            migrationBuilder.RenameTable(
                name: "hotels",
                newName: "Hotels");

            migrationBuilder.RenameTable(
                name: "countries",
                newName: "Countries");

            migrationBuilder.RenameIndex(
                name: "IX_hotels_CountryId",
                table: "Hotels",
                newName: "IX_Hotels_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hotels",
                table: "Hotels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Countries_CountryId",
                table: "Hotels",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Countries_CountryId",
                table: "Hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hotels",
                table: "Hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "Hotels",
                newName: "hotels");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "countries");

            migrationBuilder.RenameIndex(
                name: "IX_Hotels_CountryId",
                table: "hotels",
                newName: "IX_hotels_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hotels",
                table: "hotels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_countries",
                table: "countries",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_hotels_countries_CountryId",
                table: "hotels",
                column: "CountryId",
                principalTable: "countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
