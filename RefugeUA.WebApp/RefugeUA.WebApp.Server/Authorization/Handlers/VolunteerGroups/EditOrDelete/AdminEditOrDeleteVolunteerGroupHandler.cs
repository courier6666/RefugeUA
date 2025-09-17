using Microsoft.AspNetCore.Authorization;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.VolunteerGroups.EditOrDelete
{
    public class AdminEditOrDeleteVolunteerGroupHandler : AuthorizationHandler<EditOrDeleteVolunteerGroupRequirement, VolunteerGroupResult>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeleteVolunteerGroupRequirement requirement, VolunteerGroupResult resource)
        {
            return AuthorizeAdminHandlerMethod.Authorize(context, requirement);
        }
    }
}
