using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteMonitoringTool.Migrations
{
    public partial class WebSiteStatusLastActionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastActionStateId",
                table: "WebSiteStatuses",
                newName: "LastActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastActionId",
                table: "WebSiteStatuses",
                newName: "LastActionStateId");
        }
    }
}
