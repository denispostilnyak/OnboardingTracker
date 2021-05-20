namespace OnBoardingTracker.Infrastructure.FileStorage.Options
{
    public class AmazonS3FileStorageOptions
    {
        public static string Section => "Infrastructure:Storage:AmazonS3";

        public string BucketName { get; set; }

        public string AccessKeyId { get; set; }

        public string SecretAccessKey { get; set; }

        public string Region { get; set; }

        public int SignedUrlExpirationInMinutes { get; set; }
    }
}
