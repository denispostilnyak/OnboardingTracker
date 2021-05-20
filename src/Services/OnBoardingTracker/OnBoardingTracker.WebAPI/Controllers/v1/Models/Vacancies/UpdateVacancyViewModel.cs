using Microsoft.AspNetCore.Http;

namespace OnBoardingTracker.WebApi.Controllers.v1.Models
{
    public class UpdateVacancyViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal MaxSalary { get; set; }

        public int AssignedRecruiterId { get; set; }

        public double WorkExperience { get; set; }

        public int SeniorityLevelId { get; set; }

        public int JobTypeId { get; set; }

        public int VacancyStatusId { get; set; }

        public IFormFile VacancyPicture { get; set; }
    }
}
