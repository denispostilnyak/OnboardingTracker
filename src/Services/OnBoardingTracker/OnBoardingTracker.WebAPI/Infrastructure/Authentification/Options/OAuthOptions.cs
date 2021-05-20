namespace OnBoardingTracker.WebApi.Infrastructure.Authentification.Options
{
    public class OAuthOptions
    {
        public static string Section => "OAuth2";

        public string Domain { get; set; }

        public string Audience { get; set; }

        public int CacheTokenSec { get; set; }
    }
}
