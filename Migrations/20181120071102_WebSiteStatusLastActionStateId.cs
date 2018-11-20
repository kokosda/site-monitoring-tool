using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteMonitoringTool.Migrations
{
    public partial class WebSiteStatusLastActionStateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LastActionStateId",
                table: "WebSiteStatuses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastActionStateId",
                table: "WebSiteStatuses");
        }
    }
}
