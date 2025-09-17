
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.PagedList
{
    public class PagedMyVolunteerEventsList : IFeatureEndpoint
    {
        public static async Task<IResult> PagedVolunteerEventsListAsync(
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IValidator<PagedMyVolunteerEventsListQuery> validator,
            [AsParameters] PagedMyVolunteerEventsListQuery query,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var userId = httpContextAccessor.HttpContext?.User.GetId();

            var volunteerEvents = dbContext.VolunteerEvents.AsNoTracking().
                Include(e => e.Organizers).
                Include(e => e.Address).
                Include(e => e.VolunteerGroup).
                Where(e => e.Organizers.Any(p => p.Id == userId)).
                AsQueryable();

            if (query.Prompt != null)
            {
                var promptUpper = query.Prompt.ToUpper();
                volunteerEvents = volunteerEvents.Where(e => e.Title.ToUpper().Contains(promptUpper));
            }

            var totalCount = await volunteerEvents.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No volunteer events found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedVolunteerEvents = await volunteerEvents.
                OrderByDescending(a => a.CreatedAt).
                Select(VolunteerEventResultMapping.BaseResultExpression).
                Skip((page - 1) * query.PageLength).
                Take(query.PageLength).
                ToListAsync();

            var pagingInfo = new PagingInfo<VolunteerEventBaseResult>(pagedVolunteerEvents, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/volunteer/events/mine/paged", PagedVolunteerEventsListAsync)
                .Produces<PagingInfo<VolunteerEventBaseResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("PagedListMyVolunteerEvents")
                .WithTags("Volunteer");
        }
    }
}
