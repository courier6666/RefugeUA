using Microsoft.AspNetCore.Authorization;
using RefugeUA.Entities.Abstracts;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;
using System.Net;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.Announcements.EditOrDelete
{
    public class OwnerEditOrDeleteAnnouncementHandler : AuthorizationHandler<EditOrDeleteAnnouncementRequirement, BaseAnnouncementResult>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeleteAnnouncementRequirement requirement, BaseAnnouncementResult resource)
        {
            if (context.User.GetId() == resource.AuthorId)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
