using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.VolunteerEvents.EditOrDelete
{
    public class GroupAdminEditOrDeleteVolunteerEventHandler : AuthorizationHandler<EditOrDeleteVolunteerEventRequirement, VolunteerEventBaseResult>
    {
        private readonly RefugeUADbContext dbContext;

        public GroupAdminEditOrDeleteVolunteerEventHandler(RefugeUADbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeleteVolunteerEventRequirement requirement, VolunteerEventBaseResult resource)
        {
            var userId = context.User.GetId();

            if (userId == null)
            {
                return Task.CompletedTask;
            }

            var isGroupAdmin = this.dbContext.VolunteerGroups.
                AsNoTracking().
                Include(g => g.Admins).
                Where(g => g.Id == resource.VolunteerGroupId).
                SelectMany(g => g.Admins).
                Any(u => u.Id == userId);

            if(isGroupAdmin)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
