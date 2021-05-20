using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnBoardingTracker.Application.Candidates.Commands;
using OnBoardingTracker.Application.Infrastructure;
using OnBoardingTracker.Application.Infrastructure.AutoMapper.Extensions;
using OnBoardingTracker.Infrastructure.CurrentUser;
using OnBoardingTracker.Infrastructure.EmailService;
using OnBoardingTracker.Infrastructure.EmailService.Abstract;
using OnBoardingTracker.Infrastructure.EmailService.Options;
using OnBoardingTracker.Infrastructure.FileStorage;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Infrastructure.FileStorage.Options;
using OnBoardingTracker.Persistence;
using OnBoardingTracker.WebApi.Infrastructure.Authentification.Extensions;
using OnBoardingTracker.WebApi.Infrastructure.Authentification.Middleware;
using OnBoardingTracker.WebApi.Infrastructure.Authentification.Options;
using OnBoardingTracker.WebApi.Infrastructure.Authorization.Extensions;
using OnBoardingTracker.WebApi.Infrastructure.Authorization.Handlers;
using OnBoardingTracker.WebApi.Infrastructure.Authorization.Requirements;
using OnBoardingTracker.WebApi.Infrastructure.Data.Extensions;
using OnBoardingTracker.WebApi.Infrastructure.ExceptionHandling.Middleware;
using OnBoardingTracker.WebApi.Infrastructure.FileStorage.Extensions;
using OnBoardingTracker.WebApi.Infrastructure.Swagger.Extensions;
using OnBoardingTracker.WebApi.Infrastructure.Swagger.Options;
using Serilog;
using System.Reflection;

namespace OnBoardingTracker.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration).CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMetrics();

            var mediatrAssembly = typeof(CreateCandidate).GetTypeInfo().Assembly;

            services.AddMediatR(mediatrAssembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddFluentValidation(new[] { mediatrAssembly });
            services.AddCustomAutoMapper();
            services.AddDatabase(Configuration);

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwagger();

            services.AddOptions<ClientOAuth2Options>()
                    .Bind(Configuration.GetSection(ClientOAuth2Options.Section));
            services.AddOptions<OAuthOptions>()
                .Bind(Configuration.GetSection(OAuthOptions.Section));

            services.AddAmazonS3FileStorage(Configuration);

            services.AddCustomAuthentication(Configuration);

            services.AddScoped<IUserContext, UserContext>();
            services.AddSingleton<IAuthorizationHandler, HasScopeAuthorizationHandler>();

            services.AddEmailNotification(Configuration);

            services.AddCors();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddApiVersioning(
               options =>
               {
                   options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                   options.ReportApiVersions = true;
               });

            services.AddVersionedApiExplorer(
                options =>
                {
                    //Format: vMajorVersion.MinorVersion
                    options.GroupNameFormat = "'v'VV";
                    options.SubstituteApiVersionInUrl = true;
                });
            services.AddAuthorization(options => options.AddPolicy(
                "HasHelloWorldScope",
                policy => policy.Requirements.Add(new HasScopeRequirement("read.helloworld"))));

            services.AddHealthChecks().AddDbContextCheck<OnboardingTrackerContext>(tags: new[] { "db" });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Token-Expired")
                .AllowCredentials()
                .WithOrigins("http://localhost:4200", "http://devoxobtrstg.z16.web.core.windows.net"));

            app.UseCustomSwagger();
            app.MigrateDatabase(Configuration);
            app.SeedDatabase(Configuration);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<CurrentUserAllowedMiddleware>();

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health").RequireAuthorization();
                endpoints.MapHealthChecks("api/health/db", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("db")
                }).RequireAuthorization();
            });
        }
    }
}
