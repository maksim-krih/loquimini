using Microsoft.EntityFrameworkCore.Migrations;

namespace Loquimini.Data.Migrations
{
    public partial class FlatOnDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingInfo_Flat_FlatId",
                table: "BuildingInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingInfo_Flat_FlatId",
                table: "BuildingInfo",
                column: "FlatId",
                principalTable: "Flat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingInfo_Flat_FlatId",
                table: "BuildingInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingInfo_Flat_FlatId",
                table: "BuildingInfo",
                column: "FlatId",
                principalTable: "Flat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
