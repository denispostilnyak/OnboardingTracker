using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using OnBoardingTracker.WebApi.Infrastructure.Swagger.OperationFilters;
using OnBoardingTracker.WebApi.Infrastructure.Swagger.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OnBoardingTracker.WebApi.Infrastructure.Swagger.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AddDefaultVersionHeaderOperationFilter>();
                var serviceProvider = services.BuildServiceProvider();
                var provider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Title = $"API {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = description.IsDeprecated ? "Deprecated API Version" : string.Empty,
                    });
                }

                // Use xml docs in controllers to generate Swagger UI
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                var oauth2Config = serviceProvider.GetService<IOptions<ClientOAuth2Options>>()?.Value;
                if (oauth2Config != null)
                {
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                TokenUrl = new Uri(oauth2Config.OAuthTokenUrl),
                                AuthorizationUrl = new Uri(oauth2Config.AuthorizeUrl),
                                Scopes = oauth2Config.Scopes
                            }
                        }
                    });
                }
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                            Id = "oauth2", Type = ReferenceType.SecurityScheme
                        }
                    },
                        new List<string>()
                    }
                });
            });
            return services;
        }
    }
}
