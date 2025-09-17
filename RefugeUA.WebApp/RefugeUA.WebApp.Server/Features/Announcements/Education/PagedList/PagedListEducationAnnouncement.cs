using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;
using static RefugeUA.WebApp.Server.Features.Announcements.Education.Common.ListEducationAnnouncementMapping;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.PagedList
{
    public class PagedListEducationAnnouncement : IFeatureEndpoint 
    {
        public static async Task<IResult> PagedListEducationAnnouncementAsync(
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IValidator<PagedListEducationAnnouncementQuery> validator,
            [AsParameters] PagedListEducationAnnouncementQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var educationAnnouncements = dbContext.EducationAnnouncements
                .AsNoTracking()
                .Include(x => x.Address)
                .Include(x => x.Author)
                .Include(x => x.Responses)
                .Include(x => x.Groups)
                .AsQueryable()
                .Where(x => x.Accepted);

            if (query.Prompt != null)
            {
                var promptUpper = query.Prompt.ToUpper();
                educationAnnouncements = educationAnnouncements.
                    Where(a => a.Title.ToUpper().Contains(promptUpper));
            }

            if (query.IsClosed != null)
            {
                educationAnnouncements = educationAnnouncements.Where(a => a.IsClosed == query.IsClosed);
            }

            if (query.District != null)
            {
                educationAnnouncements = educationAnnouncements.Where(a => a.Address.District == query.District);
            }

            if (query.AnnouncementGroup != null)
            {
                educationAnnouncements = educationAnnouncements
                    .Where(a => a.Groups.Any(g => g.Name.ToUpper().Contains(query.AnnouncementGroup.ToUpper())));
            }

            if (query.IsFreeOnly == null || !(query.IsFreeOnly ?? true))
            {
                if (query.FeeLower != null)
                {
                    educationAnnouncements = educationAnnouncements
                        .Where(a => a.Fee != null && (a.Fee >= query.FeeLower));
                }

                if (query.FeeUpper != null)
                {
                    educationAnnouncements = educationAnnouncements
                        .Where(a => a.Fee != null && (a.Fee <= query.FeeUpper));
                }
            }
            else
            {
                educationAnnouncements = educationAnnouncements
                    .Where(a => a.Fee == null);
            }

            if (query.DurationLower != null)
            {
                educationAnnouncements = educationAnnouncements
                    .Where(a => a.Duration >= query.DurationLower);
            }

            if (query.DurationUpper != null)
            {
                educationAnnouncements = educationAnnouncements
                    .Where(a => a.Duration <= query.DurationUpper);
            }

            if (query.Language != null)
            {
                educationAnnouncements = educationAnnouncements
                    .Where(a => a.Language == query.Language);
            }

            if (query.EducationTypes != null && query.EducationTypes.Length > 0)
            {
                educationAnnouncements = educationAnnouncements
                    .Where(a => query.EducationTypes.Contains(a.EducationType));
            }

            if (query.TargetGroups != null && query.TargetGroups.Length > 0)
            {
                educationAnnouncements = educationAnnouncements
                    .Where(a => query.TargetGroups.Any(tg => a.TargetGroup.Contains(tg)));
            }

            var totalCount = await educationAnnouncements.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No education announcements found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedEducationAnnouncements = await educationAnnouncements.
                OrderByDescending(a => a.CreatedAt).
                Select(Expression).
                Skip((page - 1) * query.PageLength).
                Take(query.PageLength).
                ToListAsync();

            var pagingInfo = new PagingInfo<EducationAnnouncementResult>(pagedEducationAnnouncements, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/education/paged", PagedListEducationAnnouncementAsync)
                .Produces<List<EducationAnnouncementResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("PagedListEducationAnnouncement")
                .WithTags("EducationAnnouncements");
        }
    }
}
