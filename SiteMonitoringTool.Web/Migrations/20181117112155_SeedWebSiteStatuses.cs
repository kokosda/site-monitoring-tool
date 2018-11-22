using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteMonitoringTool.Migrations
{
    public partial class SeedWebSiteStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO WebSiteStatuses(SiteName, Url, IsActive)
                SELECT 'ePayments', 'https://www.epayments.com/', 0 UNION ALL
                SELECT 'ExampleNeverActive.Com', 'http://exampleneveractive.com/', 0 UNION ALL
                SELECT 'yandex.ru', 'http://yandex.ru/', 0"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM WebSiteStatuses
                WHERE SiteName IN ('ePayments', 'ExampleNeverActive.Com', 'yandex.ru')"
            );
        }
    }
}
