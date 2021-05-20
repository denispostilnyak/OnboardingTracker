using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnBoardingTracker.Infrastructure.EmailService;
using OnBoardingTracker.Infrastructure.EmailService.Abstract;
using OnBoardingTracker.Infrastructure.EmailService.Options;

namespace OnBoardingTracker.WebApi.Infrastructure.Authorization.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEmailNotification(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddOptions<EmailOptions>()
                .Bind(configuration.GetSection(EmailOptions.Section));

            return services;
        }
    }
}
