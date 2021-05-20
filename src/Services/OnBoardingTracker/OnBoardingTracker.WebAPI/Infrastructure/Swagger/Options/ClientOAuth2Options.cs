using System.Collections.Generic;

namespace OnBoardingTracker.WebApi.Infrastructure.Swagger.Options
{
    public class ClientOAuth2Options
    {
        public static string Section => "Swagger:Authorization:Implicit";

        public string Domain { get; set; }

        public string Audience { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string AuthorizeUrl { get; set; }

        public string OAuthTokenUrl { get; set; }

        public Dictionary<string, string> Scopes { get; set; }
    }
}
