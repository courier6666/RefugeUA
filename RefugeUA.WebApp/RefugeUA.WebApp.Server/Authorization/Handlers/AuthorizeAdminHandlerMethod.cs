using Microsoft.AspNetCore.Authorization;
using RefugeUA.WebApp.Server.Authorization.Constants;

namespace RefugeUA.WebApp.Server.Authorization.Handlers
{
    public static class AuthorizeAdminHandlerMethod
    {
        public static Task Authorize(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
        {
            if (context.User.IsInRole(Roles.Admin))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
