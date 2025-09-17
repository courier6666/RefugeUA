
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Followed
{
    public class PagedFollowedVolunteerGroupsList : IFeatureEndpoint
    {
        public static async Task<IResult> PagedFollowedVolunteerGroupsListAsync(
            [AsParameters] PagedVolunteerGroupsListQuery query,
            [FromServices] IValidator<PagedVolunteerGroupsListQuery> validator,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            var volunteerGroups = dbContext.VolunteerGroups.
                Include(g => g.Admins).
                Include(g => g.VolunteerEvents).
                Include(g => g.Followers).
                AsQueryable().
                Where(g => g.Followers.Any(a => a.Id == userId));

            if (query.Prompt != null)
            {
                var promptUpper = query.Prompt.ToUpper();
                volunteerGroups = volunteerGroups.Where(e => e.Title.ToUpper().Contains(promptUpper));
            }

            var totalCount = await volunteerGroups.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No volunteer groups found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedVolunteerGroups = await volunteerGroups.
                OrderByDescending(a => a.CreatedAt).
                Select(VolunteerGroupResultMapping.Expression).
                Skip((page - 1) * query.PageLength).
                Take(query.PageLength).
                ToListAsync();

            var pagingInfo = new PagingInfo<VolunteerGroupResult>(pagedVolunteerGroups, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/volunteer/groups/followed/paged", PagedFollowedVolunteerGroupsListAsync)
                .Produces<PagingInfo<VolunteerGroupResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("PagedListFollowedVolunteerGroups")
                .WithTags("Volunteer");
        }
    }
}
