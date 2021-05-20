using System;

namespace OnBoardingTracker.Application.Recruiters.Models
{
    public class RecruiterModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Uri PictureUrl { get; set; }
    }
}
