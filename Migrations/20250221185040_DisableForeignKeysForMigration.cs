using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyCommerce.Migrations
{
    public partial class DisableForeignKeysForMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("PRAGMA foreign_keys = 0;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("PRAGMA foreign_keys = 1;");
        }
    }
}
