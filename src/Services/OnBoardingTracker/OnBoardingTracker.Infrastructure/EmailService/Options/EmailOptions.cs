namespace OnBoardingTracker.Infrastructure.EmailService.Options
{
    public class EmailOptions
    {
        public static string Section => "Email";

        public string SmtpHost { get; set; }

        public string SmtpPort { get; set; }

        public string SmtpUser { get; set; }

        public string SmtpPassword { get; set; }

        public string FromEmail { get; set; }
    }
}
