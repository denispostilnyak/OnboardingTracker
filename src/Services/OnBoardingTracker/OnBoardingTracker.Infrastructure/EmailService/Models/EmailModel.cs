using System.Collections.Generic;

namespace OnBoardingTracker.Infrastructure.Services.SendGrid.Models
{
    public class EmailModel
    {
        public string From { get; set; }

        public IEnumerable<string> To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
