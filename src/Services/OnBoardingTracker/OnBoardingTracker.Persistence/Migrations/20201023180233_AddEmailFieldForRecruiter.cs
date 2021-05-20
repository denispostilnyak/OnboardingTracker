using Microsoft.EntityFrameworkCore.Migrations;

namespace OnBoardingTracker.Persistence.Migrations
{
    public partial class AddEmailFieldForRecruiter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Recruiters",
                maxLength: 100,
                nullable: false,
                defaultValue: string.Empty);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Recruiters");
        }
    }
}
