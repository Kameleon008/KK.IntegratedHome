using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KK.IH.Tools.Database.PostgresClient.Migrations
{
    public partial class DeviceUpdate_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Devices",
                type: "uuid",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Devices");
        }
    }
}
