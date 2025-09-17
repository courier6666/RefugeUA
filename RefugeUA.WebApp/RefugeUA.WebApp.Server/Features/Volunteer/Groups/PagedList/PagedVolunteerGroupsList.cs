
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;
using RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.PagedList
{
    public class PagedVolunteerGroupsList : IFeatureEndpoint
    {
        public static async Task<IResult> PagedVolunteerGroupsListAsync(
            [AsParameters] PagedVolunteerGroupsListQuery query,
            [FromServices] IValidator<PagedVolunteerGroupsListQuery> validator,
            [FromServices] RefugeUADbContext dbContext)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var volunteerGroups = dbContext.VolunteerGroups.
                Include(g => g.Admins).
                Include(g => g.VolunteerEvents).
                Include(g => g.Followers).
                AsQueryable();

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
            app.MapGet("api/volunteer/groups/paged", PagedVolunteerGroupsListAsync)
                .Produces<PagingInfo<VolunteerEventBaseResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("PagedListVolunteerGroups")
                .WithTags("Volunteer");
        }
    }
}
