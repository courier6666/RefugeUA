using Microsoft.AspNetCore.Authorization;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.VolunteerEvents.EditOrDelete
{
    public class AuthorEditOrDeleteVolunteerEventHandler : AuthorizationHandler<EditOrDeleteVolunteerEventRequirement, VolunteerEventBaseResult>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeleteVolunteerEventRequirement requirement, VolunteerEventBaseResult resource)
        {
            if(resource.Organizers.Any(o => o.Id == context.User.GetId()))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
