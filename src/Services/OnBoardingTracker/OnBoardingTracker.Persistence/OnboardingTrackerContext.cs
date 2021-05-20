using Microsoft.EntityFrameworkCore;
using OnBoardingTracker.Domain.Entities;
using OnBoardingTracker.Infrastructure.CurrentUser;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Persistence
{
    public class OnboardingTrackerContext : DbContext
    {
        private readonly IUserContext userContext;

        public OnboardingTrackerContext()
        {
        }

        public OnboardingTrackerContext(DbContextOptions<OnboardingTrackerContext> options, IUserContext userContext)
            : base(options)
        {
            this.userContext = userContext;
        }

        public virtual DbSet<CandidateOrigin> CandidateOrigins { get; set; }

        public virtual DbSet<CandidateSkill> CandidateSkills { get; set; }

        public virtual DbSet<CandidateVacancy> CandidateVacancies { get; set; }

        public virtual DbSet<Candidate> Candidates { get; set; }

        public virtual DbSet<Interview> Interviews { get; set; }

        public virtual DbSet<JobType> JobTypes { get; set; }

        public virtual DbSet<Recruiter> Recruiters { get; set; }

        public virtual DbSet<SeniorityLevel> SeniorityLevels { get; set; }

        public virtual DbSet<Skill> Skills { get; set; }

        public virtual DbSet<Log> Logs { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Vacancy> Vacancies { get; set; }

        public virtual DbSet<VacancySkill> VacancySkills { get; set; }

        public virtual DbSet<VacancyStatus> VacancyStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entityEntry in ChangeTracker.Entries().Where(it => it.Entity is EntityBase)
                .Where(it => it.State == EntityState.Modified ||
                it.State == EntityState.Added || it.State == EntityState.Deleted))
            {
                var entity = (EntityBase)entityEntry.Entity;
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entity.Created = DateTime.UtcNow;
                        entity.CreatedBy = userContext.Id;
                        break;
                    case EntityState.Modified:
                        entity.Updated = DateTime.UtcNow;
                        entity.UpdatedBy = userContext.Id;
                        break;
                    case EntityState.Deleted:
                        entity.IsDeleted = true;
                        entityEntry.State = EntityState.Modified;
                        break;
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}