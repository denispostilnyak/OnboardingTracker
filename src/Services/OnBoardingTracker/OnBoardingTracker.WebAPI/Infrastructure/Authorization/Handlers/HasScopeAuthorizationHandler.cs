using Microsoft.AspNetCore.Authorization;
using OnBoardingTracker.WebApi.Infrastructure.Authorization.Requirements;
using System;
using System.Threading.Tasks;

namespace OnBoardingTracker.WebApi.Infrastructure.Authorization.Handlers
{
    public class HasScopeAuthorizationHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context).ToString());
            }

            if (!context.User.HasClaim(c => c.Type == "scope"))
            {
                return Task.CompletedTask;
            }

            if (requirement == null)
            {
                throw new ArgumentNullException(nameof(requirement).ToString());
            }

            var scope = context.User.FindFirst(c => c.Type == "scope")?.Value;
            if (scope == null || !scope.Contains(requirement.RequiredScopes))
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
