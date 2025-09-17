using Microsoft.AspNetCore.Authorization;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.VolunteerEvents.EditOrDelete
{
    public class CommunityAdminEditOrDeleteVolunteerEventHandler : AuthorizationHandler<EditOrDeleteVolunteerEventRequirement, VolunteerEventBaseResult>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeleteVolunteerEventRequirement requirement, VolunteerEventBaseResult resource)
        {
            if(context.User.IsInRole(Roles.CommunityAdmin) && context.User.GetDistrict() == resource.Address?.District)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
