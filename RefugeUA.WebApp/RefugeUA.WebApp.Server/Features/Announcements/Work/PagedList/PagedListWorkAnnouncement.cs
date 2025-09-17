using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.Announcements.Work.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;
using static RefugeUA.WebApp.Server.Features.Announcements.Work.Common.ListWorkAnnouncementMappingExpression;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.PagedList
{
    public class PagedListWorkAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> PagedListWorkAnnouncementAsync(
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IValidator<PagedListEducationAnnouncementQuery> validator,
            [AsParameters] PagedListEducationAnnouncementQuery query)
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
                workAnnouncements = workAnnouncements.Where(a => a.Address.District.ToUpper().Contains(query.District.ToUpper()));
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
                        .Where(a => a.SalaryLower != null && (a.SalaryLower >= query.SalaryLower || a.SalaryUpper >= query.SalaryLower));
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

            var totalCount = await workAnnouncements.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No work announcements found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedWorkAnnouncements = await workAnnouncements.
                OrderByDescending(a => a.CreatedAt).
                Select(ListWorkAnnouncementToDtoExpression).
                Skip((page - 1) * query.PageLength).
                Take(query.PageLength).
                ToListAsync();

            var pagingInfo = new PagingInfo<WorkAnnouncementResult>(pagedWorkAnnouncements, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/work/paged", PagedListWorkAnnouncementAsync).Produces<List<WorkAnnouncementResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("PagedListWorkAnnouncement")
                .WithTags("WorkAnnouncements");
        }
    }
}