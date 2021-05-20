using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Infrastructure.CurrentUser;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Persistence;
using System;

namespace OnBoardingTracker.Application.Tests
{
    public class BaseTest : IDisposable
    {
        protected readonly OnboardingTrackerContext dbContext;

        protected readonly IFileStorage fileStorage;

        private readonly UserContext userContext;

        public BaseTest()
        {
            userContext = InitFakeUser();
            dbContext = InitFakeDbContext();
            fileStorage = A.Fake<IFileStorage>();
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        public UserContext InitFakeUser()
        {
            return new UserContext
            {
                Id = 1,
                Email = "admin@gmail.com",
                FirstName = "admin",
                LastName = "adminovich"
            };
        }

        public OnboardingTrackerContext InitFakeDbContext()
        {
            var options = new DbContextOptionsBuilder<OnboardingTrackerContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new OnboardingTrackerContext(options, userContext);
            context.Database.EnsureCreated();

            SeedDatabase(context);
            context.SaveChanges();

            return context;
        }

        public void SeedDatabase(OnboardingTrackerContext ctx)
        {
            ctx.CandidateOrigins.AddRange(
                new CandidateOrigin()
                {
                    Name = "Work.ua",
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                }, new CandidateOrigin()
                {
                    Name = "Social Networks",
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                }, new CandidateOrigin()
                {
                    Name = "Other",
                    CreatedBy = 3,
                    Created = DateTime.UtcNow
                });

            ctx.Candidates.AddRange(
                new Candidate()
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    PhoneNumber = "+3801234567890",
                    Email = "jane.doe@contoso.com",
                    OriginId = 1,
                    YearsOfExperience = 2.0,
                    CurrentJobInformation = string.Empty,
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                }, new Candidate()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "+3801234567890",
                    Email = "john.doe@contoso.com",
                    OriginId = 1,
                    YearsOfExperience = 1.0,
                    CurrentJobInformation = string.Empty,
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                }, new Candidate()
                {
                    FirstName = "Albert",
                    LastName = "Einstein",
                    PhoneNumber = "+38009876543210",
                    Email = "albert.einstein@contoso.com",
                    OriginId = 2,
                    YearsOfExperience = 5.0,
                    CurrentJobInformation = string.Empty,
                    CreatedBy = 3,
                    Created = DateTime.UtcNow
                }, new Candidate()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    PhoneNumber = "+38009876543210",
                    Email = "john.smith@contoso.com",
                    OriginId = 1,
                    YearsOfExperience = 2.0,
                    CurrentJobInformation = string.Empty,
                    CreatedBy = 3,
                    Created = DateTime.UtcNow
                });

            ctx.JobTypes.AddRange(
                new JobType()
                {
                    Name = "Full time",
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                }, new JobType()
                {
                    Name = "Remote",
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                },
                new JobType()
                {
                    Name = "Part time",
                    CreatedBy = 3,
                    Created = DateTime.UtcNow
                });

            ctx.Recruiters.AddRange(
                 new Recruiter()
                 {
                     FirstName = "Anton",
                     LastName = "Sokolov",
                     Email = "anton@gmail.com",
                     CreatedBy = 1,
                     Created = DateTime.UtcNow
                 }, new Recruiter()
                 {
                     FirstName = "Mary",
                     LastName = "Jane",
                     Email = "mary@gmail.com",
                     CreatedBy = 2,
                     Created = DateTime.UtcNow
                 }, new Recruiter()
                 {
                     FirstName = "Dasha",
                     LastName = "Bukina",
                     Email = "daria@gmail.com",
                     CreatedBy = 3,
                     Created = DateTime.UtcNow
                 }, new Recruiter()
                 {
                     FirstName = "John",
                     LastName = "Doe",
                     Email = "john.doe@gmail.com",
                     CreatedBy = 2,
                     Created = DateTime.UtcNow
                 });

            ctx.SeniorityLevels.AddRange(
                new SeniorityLevel()
                {
                    Name = "Trainee",
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                }, new SeniorityLevel()
                {
                    Name = "Junior",
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                }, new SeniorityLevel()
                {
                    Name = "Middle",
                    CreatedBy = 3,
                    Created = DateTime.UtcNow
                }, new SeniorityLevel()
                {
                    Name = "Senior",
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                });

            ctx.Skills.AddRange(
                new Domain.Entities.Skill()
                {
                    Name = "Asp.Net Core",
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                }, new Domain.Entities.Skill()
                {
                    Name = "Java",
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                }, new Domain.Entities.Skill()
                {
                    Name = "Android",
                    CreatedBy = 3,
                    Created = DateTime.UtcNow
                }, new Domain.Entities.Skill()
                {
                    Name = "SQL",
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                }, new Domain.Entities.Skill()
                {
                    Name = "Postgres",
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                });

            ctx.VacancyStatuses.AddRange(
                new Domain.Entities.VacancyStatus()
                {
                    Name = "Open",
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                }, new Domain.Entities.VacancyStatus()
                {
                    Name = "Pending",
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                }, new Domain.Entities.VacancyStatus()
                {
                    Name = "Closed",
                    CreatedBy = 3,
                    Created = DateTime.UtcNow
                });

            ctx.Vacancies.AddRange(
                new Domain.Entities.Vacancy()
                {
                    Title = "Junior Java Developer",
                    MaxSalary = 500,
                    AssignedRecruiterId = 1,
                    WorkExperience = 1.0,
                    SeniorityLevelId = 2,
                    JobTypeId = 2,
                    VacancyStatusId = 2,
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                }, new Domain.Entities.Vacancy()
                {
                    Title = "Senior .NET Developer",
                    MaxSalary = 500,
                    AssignedRecruiterId = 3,
                    WorkExperience = 4.0,
                    SeniorityLevelId = 4,
                    JobTypeId = 1,
                    VacancyStatusId = 1,
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                }, new Domain.Entities.Vacancy()
                {
                    Title = "CTO",
                    MaxSalary = 4000,
                    AssignedRecruiterId = 1,
                    WorkExperience = 1.0,
                    SeniorityLevelId = 2,
                    JobTypeId = 2,
                    VacancyStatusId = 2,
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                });

            ctx.VacancySkills.AddRange(
                new VacancySkill()
                {
                    VacancyId = 1,
                    SkillId = 2,
                }, new VacancySkill()
                {
                    VacancyId = 1,
                    SkillId = 5
                }, new VacancySkill()
                {
                    VacancyId = 2,
                    SkillId = 1,
                }, new VacancySkill()
                {
                    VacancyId = 2,
                    SkillId = 4
                });

            ctx.CandidateSkills.AddRange(
                new CandidateSkill()
                {
                    CandidateId = 1,
                    SkillId = 2
                },
           new CandidateSkill()
           {
               CandidateId = 1,
               SkillId = 3
           }, new CandidateSkill()
           {
               CandidateId = 2,
               SkillId = 4,
           }, new CandidateSkill()
           {
               CandidateId = 3,
               SkillId = 1
           }, new CandidateSkill()
           {
               CandidateId = 3,
               SkillId = 4
           }, new CandidateSkill()
           {
               CandidateId = 3,
               SkillId = 5
           });

            ctx.CandidateVacancies.AddRange(
                new CandidateVacancy()
                {
                    CandidateId = 1,
                    VacancyId = 1,
                }, new CandidateVacancy()
                {
                    CandidateId = 1,
                    VacancyId = 2
                }, new CandidateVacancy()
                {
                    CandidateId = 2,
                    VacancyId = 2,
                }, new CandidateVacancy()
                {
                    CandidateId = 2,
                    VacancyId = 1,
                }, new CandidateVacancy()
                {
                    CandidateId = 3,
                    VacancyId = 1
                });

            ctx.Interviews.AddRange(
                new Domain.Entities.Interview()
                {
                    Title = "Junior Java Developer Interview",
                    CandidateId = 1,
                    VacancyId = 1,
                    RecruiterId = 1,
                    StartingTime = DateTime.UtcNow,
                    EndingTime = DateTime.UtcNow.AddHours(2),
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                }, new Domain.Entities.Interview()
                {
                    Title = "Senior .NET Developer Interview",
                    CandidateId = 2,
                    VacancyId = 2,
                    RecruiterId = 2,
                    StartingTime = DateTime.UtcNow,
                    EndingTime = DateTime.UtcNow.AddHours(3),
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                }, new Domain.Entities.Interview()
                {
                    Title = "Junior Java Developer Interview",
                    CandidateId = 3,
                    VacancyId = 1,
                    RecruiterId = 3,
                    StartingTime = DateTime.UtcNow,
                    EndingTime = DateTime.UtcNow.AddHours(2),
                    CreatedBy = 3,
                    Created = DateTime.UtcNow
                });

            ctx.Users.AddRange(
                new Domain.Entities.User()
                {
                    FirstName = "Denis",
                    LastName = "Postilniak",
                    Email = "denis.postilniak@devoxsoftware.com"
                }, new Domain.Entities.User()
                {
                    FirstName = "Davyd",
                    LastName = "Rudenko",
                    Email = "davyd.rudenko@devoxsoftware.com"
                }, new Domain.Entities.User()
                {
                    FirstName = "Sergey",
                    LastName = "Kudriashov",
                    Email = "sergey.kudriashov@devoxsoftware.com"
                });
        }
    }
}
