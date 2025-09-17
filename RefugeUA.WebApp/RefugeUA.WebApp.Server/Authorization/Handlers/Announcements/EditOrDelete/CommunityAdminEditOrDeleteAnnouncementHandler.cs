using Microsoft.AspNetCore.Authorization;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.Announcements.EditOrDelete
{
    public class CommunityAdminEditOrDeleteAnnouncementHandler : AuthorizationHandler<EditOrDeleteAnnouncementRequirement, BaseAnnouncementResult>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeleteAnnouncementRequirement requirement, BaseAnnouncementResult resource)
        {
            if (context.User.IsInRole(Roles.CommunityAdmin) &&
                context.User.GetDistrict() == resource.Address.District)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
