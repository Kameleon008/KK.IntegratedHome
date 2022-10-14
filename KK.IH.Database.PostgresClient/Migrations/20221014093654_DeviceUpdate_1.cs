using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KK.IH.Tools.Database.PostgresClient.Migrations
{
    public partial class DeviceUpdate_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Devices",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model",
                table: "Devices");
        }
    }
}
