using Microsoft.AspNetCore.Authorization;
using RefugeUA.Entities.Abstracts;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.Announcements.EditOrDelete
{
    public class AdminEditOrDeleteAnnouncementHandler : AuthorizationHandler<EditOrDeleteAnnouncementRequirement, BaseAnnouncementResult>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeleteAnnouncementRequirement requirement, BaseAnnouncementResult resource)
        {
            return AuthorizeAdminHandlerMethod.Authorize(context, requirement);
        }
    }
}
