using Microsoft.EntityFrameworkCore.Migrations;

namespace OnBoardingTracker.Persistence.Migrations
{
    public partial class Add_Description_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Vacancies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Vacancies");
        }
    }
}
