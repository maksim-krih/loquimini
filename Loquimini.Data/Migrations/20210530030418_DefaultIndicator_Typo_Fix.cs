using Microsoft.EntityFrameworkCore.Migrations;

namespace Loquimini.Data.Migrations
{
    public partial class DefaultIndicator_Typo_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewIndictor",
                table: "Receipt",
                newName: "NewIndicator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewIndicator",
                table: "Receipt",
                newName: "NewIndictor");
        }
    }
}
