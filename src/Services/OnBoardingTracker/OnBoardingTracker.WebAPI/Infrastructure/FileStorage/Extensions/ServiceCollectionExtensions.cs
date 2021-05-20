using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnBoardingTracker.Infrastructure.FileStorage;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Infrastructure.FileStorage.Options;

namespace OnBoardingTracker.WebApi.Infrastructure.FileStorage.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddImgurFileStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<ImgurFileStorageOptions>().Bind(configuration.GetSection(ImgurFileStorageOptions.Section));
            services.AddTransient<IFileStorage, ImgurFileStorage>();
            return services;
        }

        public static IServiceCollection AddAmazonS3FileStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var s3Section = AmazonS3FileStorageOptions.Section;
            services.AddOptions<AmazonS3FileStorageOptions>().Bind(configuration.GetSection(s3Section));
            services.AddTransient<IFileStorage, AmazonS3FileStorage>();
            return services;
        }

        public static IServiceCollection AddFirebaseFileStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<FirebaseFileStorageOptions>().Bind(configuration.GetSection(FirebaseFileStorageOptions.Section));
            services.AddTransient<IFileStorage, FirebaseFileStorage>();
            return services;
        }
    }
}
