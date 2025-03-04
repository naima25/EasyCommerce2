﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyCommerce.Migrations
{
    public partial class DisableForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Disable foreign key checks
            migrationBuilder.Sql("PRAGMA foreign_keys = 0;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Re-enable foreign key checks
            migrationBuilder.Sql("PRAGMA foreign_keys = 1;");
        }
    }
}
