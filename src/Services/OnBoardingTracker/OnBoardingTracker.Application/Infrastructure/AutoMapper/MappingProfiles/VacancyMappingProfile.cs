using AutoMapper;
using OnBoardingTracker.Application.Vacancy.Comands;
using OnBoardingTracker.Application.Vacancy.Models;

namespace OnBoardingTracker.Application.AutoMapper.MapperProfiles
{
    public class VacancyMappingProfile : Profile
    {
        public VacancyMappingProfile()
        {
            CreateMap<CreateVacancy, Domain.Entities.Vacancy>();
            CreateMap<VacancyModel, Domain.Entities.Vacancy>();
            CreateMap<Domain.Entities.Vacancy, VacancyModel>();
        }
    }
}
