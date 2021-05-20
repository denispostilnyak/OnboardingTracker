using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnBoardingTracker.WebApi.Controllers.v1.Models.Recruiters
{
    public class UpdateRecruiterViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IFormFile Picture { get; set; }
    }
}
