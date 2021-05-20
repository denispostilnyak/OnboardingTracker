using Auth0.AuthenticationApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using OnBoardingTracker.Application.User.Commands;
using OnBoardingTracker.Application.User.Models;
using OnBoardingTracker.Application.User.Queries;
using OnBoardingTracker.Infrastructure.CurrentUser;
using OnBoardingTracker.WebApi.Infrastructure.Caching.Extensions;
using OnBoardingTracker.WebApi.Infrastructure.Exceptions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using OAuthOptions = OnBoardingTracker.WebApi.Infrastructure.Authentification.Options.OAuthOptions;
using UserInfo = Auth0.AuthenticationApi.Models.UserInfo;

namespace OnBoardingTracker.WebApi.Infrastructure.Authentification.Middleware
{
    public class CurrentUserAllowedMiddleware
    {
        private readonly RequestDelegate next;

        public CurrentUserAllowedMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, IOptions<OAuthOptions> oAuthOptions, MediatR.IMediator mediator, IDistributedCache cache, IUserContext userContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext).ToString());
            }

            if (!httpContext.User.Identity.IsAuthenticated)
            {
                await next.Invoke(httpContext);
                return;
            }

            var claims = httpContext.User.Claims.ToList();
            var userIdentifier = claims.FirstOrDefault(
                claim => claim.Type == ClaimTypes.NameIdentifier);

            if (userIdentifier == null)
            {
                throw new UnauthorizedException("Claims can't be found");
            }

            var queryResult = await cache.GetAsync<UserModel>(userIdentifier.Value);
            if (queryResult != null)
            {
                LoadFromUserModel(queryResult);
                await next.Invoke(httpContext);
                return;
            }

            var token = claims.FirstOrDefault(claim => claim.Type == "access_token").Value;
            if (token == null)
            {
                throw new UnauthorizedException("Access token can't be found");
            }

            UserInfo userInfo = null;
            using (var apiClient = new AuthenticationApiClient(oAuthOptions.Value.Domain))
            {
                userInfo = await apiClient.GetUserInfoAsync(token);
            }

            queryResult = await mediator.Send(new GetUserByEmail { Email = userInfo.Email });
            if (queryResult != null)
            {
                await cache.SetAsync(userIdentifier.Value, queryResult, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = System.TimeSpan.FromSeconds(oAuthOptions.Value.CacheTokenSec)
                });

                LoadFromUserModel(queryResult);
                await next.Invoke(httpContext);
                return;
            }

            var user = await mediator.Send(new CreateUser
            {
                Email = userInfo.Email,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName
            });

            queryResult = new UserModel
            {
                Id = user.Id,
                Email = userInfo.Email,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName
            };

            await cache.SetAsync(userIdentifier.Value, queryResult, new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(oAuthOptions.Value.CacheTokenSec)
            });

            LoadFromUserModel(queryResult);

            await next.Invoke(httpContext);

            void LoadFromUserModel(UserModel model)
            {
                if (userContext == null)
                {
                    throw new ArgumentNullException(nameof(userContext).ToString());
                }

                userContext.Id = model.Id;
                userContext.FirstName = model.FirstName;
                userContext.LastName = model.LastName;
                userContext.Email = model.Email;
            }
        }
    }
}