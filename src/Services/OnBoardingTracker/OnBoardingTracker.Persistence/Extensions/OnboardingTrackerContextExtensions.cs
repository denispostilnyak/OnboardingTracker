using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OnBoardingTracker.Domain.Entities;
using System;

namespace OnBoardingTracker.Persistence.Extensions
{
    public static class OnboardingTrackerContextExtensions
    {
        private static bool CheckIfEmpty<T>(DbSet<T> entities)
                where T : class
        {
            return !entities.Any();
        }

        public static void Seed(this OnboardingTrackerContext ctx)
        {
            if (CheckIfEmpty(ctx.CandidateOrigins))
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
                ctx.SaveChanges();
            }

            if (CheckIfEmpty(ctx.Candidates))
            {
                ctx.Candidates.AddRange(
                    new Candidate()
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    PhoneNumber = "+3801234567890",
                    Email = "denis.postilniak@devoxsoftware.com",
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
                });
                ctx.SaveChanges();
            }

            if (CheckIfEmpty(ctx.JobTypes))
            {
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
                ctx.SaveChanges();
            }

            if (CheckIfEmpty(ctx.Recruiters))
            {
                ctx.Recruiters.AddRange(
                    new Recruiter()
                {
                    FirstName = "Anton",
                    LastName = "Sokolov",
                    Email = "sergey.kudriashov@devoxsoftware.com",
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
                });
                ctx.SaveChanges();
            }

            if (CheckIfEmpty(ctx.SeniorityLevels))
            {
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
                ctx.SaveChanges();
            }

            if (CheckIfEmpty(ctx.Skills))
            {
                ctx.Skills.AddRange(
                    new Skill()
                {
                    Name = "Asp.Net Core",
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                }, new Skill()
                {
                    Name = "Java",
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                }, new Skill()
                {
                    Name = "Android",
                    CreatedBy = 3,
                    Created = DateTime.UtcNow
                }, new Skill()
                {
                    Name = "SQL",
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                }, new Skill()
                {
                    Name = "Postgres",
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                });
                ctx.SaveChanges();
            }

            if (CheckIfEmpty(ctx.VacancyStatuses))
            {
                ctx.VacancyStatuses.AddRange(
                    new VacancyStatus()
                {
                    Name = "Open",
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                }, new VacancyStatus()
                {
                    Name = "Pending",
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                }, new VacancyStatus()
                {
                    Name = "Closed",
                    CreatedBy = 3,
                    Created = DateTime.UtcNow
                });
                ctx.SaveChanges();
            }

            if (CheckIfEmpty(ctx.Vacancies))
            {
                ctx.Vacancies.AddRange(
                    new Vacancy()
                {
                    Title = "Junior Java Developer",
                    Description = "🔸 We are looking for a person with the following skills and qualification: 🔸 2 + years of experience in Full - Stack development using .NET and Angular 2 +.",
                    MaxSalary = 500,
                    AssignedRecruiterId = 1,
                    WorkExperience = 1.0,
                    SeniorityLevelId = 2,
                    JobTypeId = 2,
                    VacancyStatusId = 2,
                    CreatedBy = 1,
                    Created = DateTime.UtcNow
                }, new Vacancy()
                {
                    Title = "Senior .NET Developer",
                    Description = "🔸 We are looking for a person with the following skills and qualification: 🔸 2 + years of experience in Full - Stack development using .NET and Angular 2 +.",
                    MaxSalary = 500,
                    AssignedRecruiterId = 3,
                    WorkExperience = 4.0,
                    SeniorityLevelId = 4,
                    JobTypeId = 1,
                    VacancyStatusId = 1,
                    CreatedBy = 2,
                    Created = DateTime.UtcNow
                });
                ctx.SaveChanges();
            }

            if (CheckIfEmpty(ctx.VacancySkills))
            {
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
                ctx.SaveChanges();
            }

            if (CheckIfEmpty(ctx.CandidateSkills))
            {
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
                ctx.SaveChanges();
            }

            if (CheckIfEmpty(ctx.CandidateVacancies))
            {
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
                ctx.SaveChanges();
            }

            if (CheckIfEmpty(ctx.Interviews))
            {
                ctx.Interviews.AddRange(
                    new Interview()
                    {
                        Title = "Junior Java Developer Interview",
                        CandidateId = 1,
                        VacancyId = 1,
                        RecruiterId = 1,
                        StartingTime = DateTime.UtcNow,
                        EndingTime = DateTime.UtcNow.AddHours(2),
                        CreatedBy = 1,
                        Created = DateTime.UtcNow
                    }, new Interview()
                    {
                        Title = "Senior .NET Developer Interview",
                        CandidateId = 2,
                        VacancyId = 2,
                        RecruiterId = 2,
                        StartingTime = DateTime.UtcNow,
                        EndingTime = DateTime.UtcNow.AddHours(3),
                        CreatedBy = 2,
                        Created = DateTime.UtcNow
                    }, new Interview()
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
                ctx.SaveChanges();
            }

            if (CheckIfEmpty(ctx.Users))
            {
                ctx.Users.AddRange(
                    new User()
                    {
                        FirstName = "Denis",
                        LastName = "Postilniak",
                        Email = "denis.postilniak@devoxsoftware.com"
                    }, new User()
                    {
                        FirstName = "Davyd",
                        LastName = "Rudenko",
                        Email = "davyd.rudenko@devoxsoftware.com"
                    }, new User()
                    {
                        FirstName = "Sergey",
                        LastName = "Kudriashov",
                        Email = "sergey.kudriashov@devoxsoftware.com"
                    });
                ctx.SaveChanges();
            }
        }
    }
}
