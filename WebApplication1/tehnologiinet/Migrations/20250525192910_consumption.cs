using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tehnologiinet.Migrations
{
    public partial class consumption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Consumptions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Consumptions");
        }
    }
}
