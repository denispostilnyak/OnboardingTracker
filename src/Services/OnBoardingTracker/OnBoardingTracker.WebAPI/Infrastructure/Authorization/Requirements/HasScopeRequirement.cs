using Microsoft.AspNetCore.Authorization;
using System;

namespace OnBoardingTracker.WebApi.Infrastructure.Authorization.Requirements
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string RequiredScopes { get; set; }

        public HasScopeRequirement(params string[] scopes)
        {
            RequiredScopes = string.Join(" ", scopes);
        }
    }
}
