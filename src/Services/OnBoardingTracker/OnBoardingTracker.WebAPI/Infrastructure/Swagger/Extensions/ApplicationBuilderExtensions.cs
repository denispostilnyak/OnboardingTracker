using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnBoardingTracker.WebApi.Infrastructure.Swagger.Options;
using System;
using System.Collections.Generic;

namespace OnBoardingTracker.WebApi.Infrastructure.Swagger.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException($"{nameof(IApplicationBuilder)} is null.");
            }

            builder.UseSwagger();
            var apiVersionDescriptionProvider = builder.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            var authOptions = builder.ApplicationServices.GetService<IOptions<ClientOAuth2Options>>()?.Value;
            builder.UseSwaggerUI(options =>
            {
                if (authOptions != null)
                {
                    options.OAuthClientId(authOptions.ClientId);
                    options.OAuthClientSecret(authOptions.ClientSecret);
                    options.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "audience", authOptions.Audience } });
                }
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName);
                }
            });
            return builder;
        }
    }
}
