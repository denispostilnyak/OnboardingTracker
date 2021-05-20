using Microsoft.EntityFrameworkCore.Migrations;

namespace OnBoardingTracker.Persistence.Migrations
{
    public partial class AddREcruiterIdFieldForInterview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecruiterId",
                table: "Interviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_RecruiterId",
                table: "Interviews",
                column: "RecruiterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Recruiters",
                table: "Interviews",
                column: "RecruiterId",
                principalTable: "Recruiters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Recruiters",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_RecruiterId",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "RecruiterId",
                table: "Interviews");
        }
    }
}
