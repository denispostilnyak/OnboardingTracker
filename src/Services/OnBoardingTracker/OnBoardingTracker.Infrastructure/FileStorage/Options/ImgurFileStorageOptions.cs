namespace OnBoardingTracker.Infrastructure.FileStorage.Options
{
    public class ImgurFileStorageOptions
    {
        public static string Section => "Infrastructure:Storage:Imgur";

        public string ClientId { get; set; }
    }
}
