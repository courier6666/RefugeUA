using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.Announcements.Work.Common;
using static RefugeUA.WebApp.Server.Features.Announcements.Work.Common.ListWorkAnnouncementMappingExpression;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.List
{
    public class ListWorkAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> ListWorkAnnouncementAsync(
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IValidator<ListWorkAnnouncementQuery> validator,
            [AsParameters] ListWorkAnnouncementQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var workAnnouncements = dbContext.WorkAnnouncements
                .AsNoTracking()
                .Include(x => x.Address)
                .Include(x => x.WorkCategory)
                .Include(x => x.Author)
                .Include(x => x.Responses)
                .Include(x => x.Groups)
                .AsQueryable()
                .Where(x => x.Accepted);

            if (query.Prompt != null)
            {
                var promptUpper = query.Prompt.ToUpper();
                workAnnouncements = workAnnouncements.
                    Where(a => a.Title.ToUpper().Contains(promptUpper) ||
                               a.JobPosition.ToUpper().Contains(promptUpper));
            }

            if (query.IsClosed != null)
            {
                workAnnouncements = workAnnouncements.Where(a => a.IsClosed == query.IsClosed);
            }

            if (query.District != null)
            {
                workAnnouncements = workAnnouncements.Where(a => a.Address.District == query.District);
            }

            if (query.AnnouncementGroup != null)
            {
                workAnnouncements = workAnnouncements
                    .Where(a => a.Groups.Any(g => g.Name.ToUpper().Contains(query.AnnouncementGroup.ToUpper())));
            }

            if (query.SalaryNotSet == null || !(query.SalaryNotSet ?? true))
            {
                if (query.SalaryLower != null)
                {
                    workAnnouncements = workAnnouncements
                        .Where(a => a.SalaryLower != null && (a.SalaryLower >= query.SalaryLower || a.SalaryUpper >= query.SalaryUpper));
                }

                if (query.SalaryUpper != null)
                {
                    workAnnouncements = workAnnouncements
                        .Where(a => a.SalaryUpper != null && (a.SalaryUpper <= query.SalaryUpper || a.SalaryLower <= query.SalaryUpper));
                }
            }
            else
            {
                workAnnouncements = workAnnouncements
                    .Where(a => a.SalaryLower == null && a.SalaryUpper == null);
            }

            if (query.JobCategories != null && query.JobCategories.Length > 0)
            {
                workAnnouncements = workAnnouncements
                    .Where(a => query.JobCategories.Any(id => a.WorkCategoryId == id));
            }

            var result = await workAnnouncements.
                OrderByDescending(a => a.CreatedAt).
                Select(ListWorkAnnouncementToDtoExpression).
                ToListAsync();

            if (result.Count == 0)
            {
                return Results.NotFound("No work announcements found with such parameters!");
            }

            return Results.Ok(result);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/work", ListWorkAnnouncementAsync).Produces<List<WorkAnnouncementResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("ListWorkAnnouncement")
                .WithTags("WorkAnnouncements");
        }
    }
}