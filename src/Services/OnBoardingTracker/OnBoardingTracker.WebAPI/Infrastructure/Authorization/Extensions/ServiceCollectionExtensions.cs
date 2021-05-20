using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using OnBoardingTracker.WebApi.Infrastructure.Authorization.Handlers;
using OnBoardingTracker.WebApi.Infrastructure.Authorization.Requirements;

namespace OnBoardingTracker.WebApi.Infrastructure.Authorization.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthorization(options => options.AddPolicy(
                "HasHelloWorldScope",
                    policy => policy.Requirements.Add(new HasScopeRequirement("read.helloworld"))));
            services.AddSingleton<IAuthorizationHandler, HasScopeAuthorizationHandler>();

            return services;
        }
    }
}
