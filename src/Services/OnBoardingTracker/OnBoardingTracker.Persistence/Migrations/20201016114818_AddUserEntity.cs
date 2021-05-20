using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnBoardingTracker.Persistence.Migrations
{
    public partial class AddUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "VacancyStatuses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "VacancyStatuses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VacancyStatuses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "VacancyStatuses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "VacancyStatuses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Vacancies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Vacancies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vacancies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Vacancies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Vacancies",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Skills",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Skills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Skills",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Skills",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Skills",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "SeniorityLevels",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "SeniorityLevels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SeniorityLevels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "SeniorityLevels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "SeniorityLevels",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Recruiters",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Recruiters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Recruiters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Recruiters",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Recruiters",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "JobTypes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "JobTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "JobTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "JobTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "JobTypes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Interviews",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Interviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Interviews",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Interviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Interviews",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Candidates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Candidates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Candidates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Candidates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Candidates",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "CandidateOrigins",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "CandidateOrigins",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CandidateOrigins",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "CandidateOrigins",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "CandidateOrigins",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "VacancyStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VacancyStatuses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VacancyStatuses");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "VacancyStatuses");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "VacancyStatuses");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "SeniorityLevels");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SeniorityLevels");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SeniorityLevels");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "SeniorityLevels");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SeniorityLevels");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Recruiters");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Recruiters");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Recruiters");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Recruiters");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Recruiters");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "JobTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "JobTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "JobTypes");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "JobTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "JobTypes");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "CandidateOrigins");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CandidateOrigins");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CandidateOrigins");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "CandidateOrigins");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CandidateOrigins");
        }
    }
}
