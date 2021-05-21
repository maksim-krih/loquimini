using Microsoft.EntityFrameworkCore.Migrations;

namespace Loquimini.Data.Migrations
{
    public partial class HouseOnDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingInfo_House_HouseId",
                table: "BuildingInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingInfo_House_HouseId",
                table: "BuildingInfo",
                column: "HouseId",
                principalTable: "House",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingInfo_House_HouseId",
                table: "BuildingInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingInfo_House_HouseId",
                table: "BuildingInfo",
                column: "HouseId",
                principalTable: "House",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
