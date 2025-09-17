
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.Admin.Announcements.Moderation.PagedList;
using RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;
using static RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement.BaseAnnouncementResultWithTypeMapping;

namespace RefugeUA.WebApp.Server.Features.Announcements.Mine
{
    public class PagedMyAnnouncementsList : IFeatureEndpoint
    {
        public static async Task<IResult> PagedMyAnnouncementsListAsync(
            [AsParameters] PagedMyAnnouncementsListQuery query,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IValidator<PagedMyAnnouncementsListQuery> validator)
        {
            var user = httpContextAccessor!.HttpContext!.User;

            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var announcements = dbContext.Announcements
                .Include(x => x.Address)
                .Include(x => x.ContactInformation)
                .Include(x => x.Responses)
                .Include(x => x.Author)
                .Include(x => x.Groups)
                .AsQueryable()
                .Where(x => x.AuthorId == user.GetId());

            var promptUpper = query.Prompt?.ToUpper() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(query.Prompt))
            {
                announcements = announcements.Where(x => x.Title.ToUpper().Contains(promptUpper) || x.Author.Email.ToUpper().Contains(promptUpper));
            }

            var totalCount = await announcements.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No announcements found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedAnnouncements = (await announcements.
                OrderByDescending(a => a.CreatedAt).
                Skip((page - 1) * query.PageLength).
                Take(query.PageLength).
                ToListAsync()).
                Select(AnnouncementToAnnouncementDtoModerationMapping).
                ToList();

            var pagingInfo = new PagingInfo<BaseAnnouncementResultWithType>(pagedAnnouncements, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/mine", PagedMyAnnouncementsListAsync)
                .Produces<PagingInfo<AccomodationAnnouncementResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("PagedMyAnnouncementsList")
                .WithTags("Announcements")
                .RequireAuthorization();
        }
    }
}
