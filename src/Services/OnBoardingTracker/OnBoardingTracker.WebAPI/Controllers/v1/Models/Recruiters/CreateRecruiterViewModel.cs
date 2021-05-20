using Microsoft.AspNetCore.Http;

namespace OnBoardingTracker.WebApi.Controllers.v1.Models
{
    public class CreateRecruiterViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IFormFile Picture { get; set; }
    }
}
