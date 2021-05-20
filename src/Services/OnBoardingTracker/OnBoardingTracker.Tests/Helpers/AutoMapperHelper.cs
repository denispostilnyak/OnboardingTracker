using AutoMapper;
using OnBoardingTracker.Application.AutoMapper.MapperProfiles;
using OnBoardingTracker.Application.Infrastructure.AutoMapper.MappingProfiles;
using System;

namespace OnBoardingTracker.Application.Tests.Helpers
{
    public static class AutoMapperHelper
    {
        private static readonly Lazy<IMapper> mapper = new Lazy<IMapper>(InitMapper);

        private static MapperConfiguration configuration;

        public static IMapper Mapper { get; set; } = mapper.Value;

        private static IMapper InitMapper()
        {
            configuration = new MapperConfiguration(cfg =>
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
            });

            return new Mapper(configuration);
        }
    }
}
