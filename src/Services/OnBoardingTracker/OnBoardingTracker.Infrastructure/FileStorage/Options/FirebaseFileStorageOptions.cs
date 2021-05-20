namespace OnBoardingTracker.Infrastructure.FileStorage.Options
{
    public class FirebaseFileStorageOptions
    {
        public static string Section => "Infrastructure:Storage:Firebase";

        public string ApiKey { get; set; }

        public string Bucket { get; set; }

        public string Folder { get; set; }
    }
}
