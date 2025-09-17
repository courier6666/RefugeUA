
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.PagedList
{
    public class PagedVoluteerEventsList : IFeatureEndpoint
    {
        public static async Task<IResult> PagedVolunteerEventsListAsync(
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IValidator<PagedVolunteerEventsListQuery> validator,
            [AsParameters] PagedVolunteerEventsListQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var volunteerEvents = dbContext.VolunteerEvents.AsNoTracking().
                Include(e => e.Organizers).
                Include(e => e.Address).
                Include(e => e.VolunteerGroup).
                AsQueryable();

            if (query.IsClosed != null)
            {
                volunteerEvents = volunteerEvents.Where(e => e.IsClosed == query.IsClosed);
            }

            if (query.Prompt != null)
            {
                var promptUpper = query.Prompt.ToUpper();
                volunteerEvents = volunteerEvents.Where(e => e.Title.ToUpper().Contains(promptUpper));
            }

            if (query.StartDate != null)
            {
                volunteerEvents = volunteerEvents.Where(e => e.StartTime >= query.StartDate);
            }

            if (query.EndDate != null)
            {
                volunteerEvents = volunteerEvents.Where(e => e.EndTime <= query.EndDate);
            }

            if (query.VolunteerGroupId != null)
            {
                volunteerEvents = volunteerEvents.Where(e => e.VolunteerGroupId == query.VolunteerGroupId);
            }

            if (query.EventType != null)
            {
                volunteerEvents = volunteerEvents.Where(e => e.EventType == query.EventType);
            }

            if (query.District != null)
            {
                volunteerEvents = volunteerEvents.Where(e => e.Address.District == query.District);
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
            app.MapGet("api/volunteer/events/paged", PagedVolunteerEventsListAsync)
                .Produces<PagingInfo<VolunteerEventBaseResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("PagedListVolunteerEvents")
                .WithTags("Volunteer");
        }
    }
}
