using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteMonitoringTool.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 31, nullable: false),
                    Password = table.Column<string>(maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT name from sys.indexes  WHERE name = N'SMT_UI_Users_Username')   
                    DROP INDEX SMT_UI_Users_Username ON Users;   
                GO
                CREATE UNIQUE INDEX SMT_UI_Users_Username
                ON [Users] (Username);
                GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT name from sys.indexes  WHERE name = N'SMT_UI_Users_Username')   
                    DROP INDEX SMT_UI_User_Username ON [Users];
            ");
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
