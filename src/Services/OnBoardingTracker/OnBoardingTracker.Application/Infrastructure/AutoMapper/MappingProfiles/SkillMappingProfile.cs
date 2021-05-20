using AutoMapper;
using OnBoardingTracker.Application.Skills.Commands;
using OnBoardingTracker.Application.Skills.Models;

namespace OnBoardingTracker.Application.Infrastructure.AutoMapper.MappingProfiles
{
    public class SkillMappingProfile : Profile
    {
        public SkillMappingProfile()
        {
            CreateMap<CreateSkill, Domain.Entities.Skill>();
            CreateMap<SkillModel, Domain.Entities.Skill>();
            CreateMap<Domain.Entities.Skill, SkillModel>();
        }
    }
}
