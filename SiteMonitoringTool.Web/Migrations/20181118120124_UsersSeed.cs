using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteMonitoringTool.Migrations
{
    public partial class UsersSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [Users] (Username, Password)
                SELECT 'admin', 'admin'
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM [Users]
                WHERE Username = 'admin'
            ");
        }
    }
}
