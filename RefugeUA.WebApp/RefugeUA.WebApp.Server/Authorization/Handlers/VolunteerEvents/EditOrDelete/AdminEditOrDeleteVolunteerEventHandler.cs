using Microsoft.AspNetCore.Authorization;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.VolunteerEvents.EditOrDelete
{
    public class AdminEditOrDeleteVolunteerEventHandler : AuthorizationHandler<EditOrDeleteVolunteerEventRequirement, VolunteerEventBaseResult>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeleteVolunteerEventRequirement requirement, VolunteerEventBaseResult resource)
        {
            if(context.User.IsInRole(Roles.Admin))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
