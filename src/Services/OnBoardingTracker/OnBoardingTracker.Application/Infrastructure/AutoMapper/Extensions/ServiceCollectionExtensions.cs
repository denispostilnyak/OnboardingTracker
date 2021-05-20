using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OnBoardingTracker.Application.AutoMapper.MapperProfiles;
using OnBoardingTracker.Application.Infrastructure.AutoMapper.MappingProfiles;
using System.Reflection;

namespace OnBoardingTracker.Application.Infrastructure.AutoMapper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
            cfg =>
            {
                cfg.AddProfile<SeniorityLevelMappingProfile>();
                cfg.AddProfile<CandidateMappingProfile>();
                cfg.AddProfile<OriginMappingProfile>();
                cfg.AddProfile<VacancyStatusMappingProfile>();
                cfg.AddProfile<InterviewMappingProfile>();
                cfg.AddProfile<VacancyMappingProfile>();
                cfg.AddProfile<RecruiterMappingProfile>();
                cfg.AddProfile<SkillMappingProfile>();
                cfg.AddProfile<JobTypeMappingProfile>();
                cfg.AddProfile<UserMappingProfile>();
            }, Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
