using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnBoardingTracker.Persistence;
using OnBoardingTracker.Persistence.Extensions;
using System;

namespace OnBoardingTracker.WebApi.Infrastructure.Data.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration).ToString());
            }

            if (bool.TryParse(configuration["Db:Seeding:Enable"], out var seed) && seed)
            {
                if (app == null)
                {
                    throw new ArgumentNullException(nameof(app).ToString());
                }

                using (var serviceScope =
                    app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<OnboardingTrackerContext>();
                    context.Seed();
                }
            }

            return app;
        }

        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (bool.TryParse(configuration["Db:Migration:Enable"], out var migrateDatabase) && migrateDatabase)
            {
                using (var serviceScope =
                    app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<OnboardingTrackerContext>();
                    context.Database.Migrate();
                }
            }

            return app;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OnboardingTrackerContext>(options =>
                options.UseSqlServer(configuration["Db:ConnectionString"]));
            return services;
        }
    }
}
