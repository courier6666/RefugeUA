using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.Entities.Interfaces;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Extensions.Authentication;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.User
{
    public class CommunityAdminEditUserHandler : AuthorizationHandler<EditUserRequirement, AppUser>
    {
        private readonly UserManager<AppUser> userManager;
        public CommunityAdminEditUserHandler(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditUserRequirement requirement, AppUser user)
        {
            if (context.User.IsInRole(Roles.CommunityAdmin) &&
                context.User.GetDistrict() == user.District &&
                !userManager.IsInRoleAsync(user, Roles.Admin).Result &&
                !userManager.IsInRoleAsync(user, Roles.CommunityAdmin).Result)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
