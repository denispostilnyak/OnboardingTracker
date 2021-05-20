using AutoMapper;
using OnBoardingTracker.Application.VacancyStatuses.Commands;
using OnBoardingTracker.Application.VacancyStatuses.Models;
using OnBoardingTracker.Domain.Entities;

namespace OnBoardingTracker.Application.Infrastructure.AutoMapper.MappingProfiles
{
    public class VacancyStatusMappingProfile : Profile
    {
        public VacancyStatusMappingProfile()
        {
            CreateMap<VacancyStatus, VacancyStatusModel>();
            CreateMap<VacancyStatusModel, VacancyStatus>();
            CreateMap<CreateVacancyStatus, VacancyStatus>();
        }
    }
}
