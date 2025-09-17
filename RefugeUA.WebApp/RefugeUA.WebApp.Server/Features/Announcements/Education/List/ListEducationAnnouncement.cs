using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;
using static RefugeUA.WebApp.Server.Features.Announcements.Education.Common.ListEducationAnnouncementMapping;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.List
{
    public class ListEducationAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> ListEducationAnnouncementAsync(
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IValidator<ListEducationAnnouncementQuery> validator,
            [AsParameters] ListEducationAnnouncementQuery query)
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
                    .Where(a => a.Duration >= query.DurationLower.Value);
            }

            if (query.DurationUpper != null)
            {
                educationAnnouncements = educationAnnouncements
                    .Where(a => a.Duration <= query.DurationUpper.Value);
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

            var result = await educationAnnouncements.
                OrderByDescending(a => a.CreatedAt).
                Select(Expression).
                ToListAsync();

            foreach (var announcement in result)
            {
                announcement.TargetGroups = announcement.TargetGroups[0].Split(';');
            }

            if (result.Count == 0)
            {
                return Results.NotFound("No education announcements found with such parameters!");
            }

            return Results.Ok(result);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/education", ListEducationAnnouncementAsync)
                .Produces<List<EducationAnnouncementResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("ListEducationAnnouncement")
                .WithTags("EducationAnnouncements");
        }
    }
}
