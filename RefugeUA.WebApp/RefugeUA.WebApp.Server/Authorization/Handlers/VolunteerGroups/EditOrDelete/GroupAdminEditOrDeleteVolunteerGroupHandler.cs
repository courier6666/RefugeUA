using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Requierements;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common;

namespace RefugeUA.WebApp.Server.Authorization.Handlers.VolunteerGroups.EditOrDelete
{
    public class GroupAdminEditOrDeleteVolunteerGroupHandler : AuthorizationHandler<EditOrDeleteVolunteerGroupRequirement, VolunteerGroupResult>
    {
        private readonly RefugeUADbContext dbContext;

        public GroupAdminEditOrDeleteVolunteerGroupHandler(RefugeUADbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EditOrDeleteVolunteerGroupRequirement requirement, VolunteerGroupResult resource)
        {
            var userId = context.User.GetId() ?? 0;
            var isAdminOfGroup = await this.dbContext.VolunteerGroups.
                Include(g => g.Admins).
                Where(g => g.Id == resource.Id).
                SelectMany(g => g.Admins).
                AnyAsync(a => a.Id == userId);

            if (isAdminOfGroup)
            {
                context.Succeed(requirement);
            }
        }
    }
}
