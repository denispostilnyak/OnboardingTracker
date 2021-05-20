using Microsoft.EntityFrameworkCore.Migrations;

namespace OnBoardingTracker.Persistence.Migrations
{
    public partial class AddedRecruiterPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Recruiters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Recruiters");
        }
    }
}
