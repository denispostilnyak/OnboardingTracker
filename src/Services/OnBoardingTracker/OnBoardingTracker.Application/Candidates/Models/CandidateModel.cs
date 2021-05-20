using System;

namespace OnBoardingTracker.Application.Candidates.Models
{
    public class CandidateModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int OriginId { get; set; }

        public double YearsOfExperience { get; set; }

        public string CurrentJobInformation { get; set; }

        public Uri CvUrl { get; set; }

        public Uri ProfilePicture { get; set; }
    }
}
