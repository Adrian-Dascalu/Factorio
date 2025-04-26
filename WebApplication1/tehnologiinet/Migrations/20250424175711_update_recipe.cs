using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tehnologiinet.Migrations
{
    public partial class update_recipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Recipes");

            migrationBuilder.AddColumn<long>(
                name: "Amount",
                table: "Recipes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Recipes");

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Recipes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
