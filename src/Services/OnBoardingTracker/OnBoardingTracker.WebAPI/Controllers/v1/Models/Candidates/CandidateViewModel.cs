using Microsoft.AspNetCore.Http;

namespace OnBoardingTracker.WebApi.Controllers.v1.Models
{
    public class CandidateViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int OriginId { get; set; }

        public double YearsOfExperience { get; set; }

        public string CurrentJobInformation { get; set; }

        public IFormFile CvFile { get; set; }

        public IFormFile ProfilePicFile { get; set; }
    }
}
