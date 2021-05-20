using Microsoft.EntityFrameworkCore.Migrations;

namespace OnBoardingTracker.Persistence.Migrations
{
    public partial class AddCascadeDeletingForCandidateVacancyAndVacancySkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancies_Candidates",
                table: "CandidateVacancies");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancies_Vacancies",
                table: "CandidateVacancies");

            migrationBuilder.DropForeignKey(
                name: "FK_VacancySkills_Skills",
                table: "VacancySkills");

            migrationBuilder.DropForeignKey(
                name: "FK_VacancySkills_Vacancies",
                table: "VacancySkills");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateVacancies_Candidates",
                table: "CandidateVacancies",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateVacancies_Vacancies",
                table: "CandidateVacancies",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VacancySkills_Skills",
                table: "VacancySkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VacancySkills_Vacancies",
                table: "VacancySkills",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancies_Candidates",
                table: "CandidateVacancies");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateVacancies_Vacancies",
                table: "CandidateVacancies");

            migrationBuilder.DropForeignKey(
                name: "FK_VacancySkills_Skills",
                table: "VacancySkills");

            migrationBuilder.DropForeignKey(
                name: "FK_VacancySkills_Vacancies",
                table: "VacancySkills");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateVacancies_Candidates",
                table: "CandidateVacancies",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateVacancies_Vacancies",
                table: "CandidateVacancies",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VacancySkills_Skills",
                table: "VacancySkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VacancySkills_Vacancies",
                table: "VacancySkills",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
