using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteMonitoringTool.Migrations
{
    public partial class WebSiteStatusInterval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Interval",
                table: "WebSiteStatuses",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, seconds: 30));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "WebSiteStatuses",
                nullable: false,
                defaultValue: DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "WebSiteStatuses");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "WebSiteStatuses");
        }
    }
}
