
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.Announcements.Common;
using RefugeUA.WebApp.Server.Features.Announcements.Responses.Common;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.Announcements.Responses.Mine.PagedList
{
    public class PagedListMyResponses : IFeatureEndpoint
    {
        public static async Task<IResult> PagedListMyResponsesAsync(
            [AsParameters] PagedListResponsesQuery query,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IValidator<PagedListResponsesQuery> validator,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var userId = httpContextAccessor?.HttpContext?.User.GetId() ?? 0;

            var responses = dbContext.AnnouncementResponses.
                Include(r => r.ContactInformation).
                Include(r => r.Announcement).
                Include(r => r.User).
                Where(r => r.UserId == userId);

            if (query.Prompt != null)
            {
                var promptUpper = query.Prompt.ToUpper();
                responses = responses.Where(r => r.Announcement.Title.ToUpper().Contains(promptUpper) ||
                    r.User.FirstName.ToUpper().Contains(promptUpper) ||
                    r.User.LastName.ToUpper().Contains(promptUpper) ||
                    r.ContactInformation.PhoneNumber.ToUpper().Contains(promptUpper) ||
                    r.ContactInformation.Email.ToUpper().Contains(promptUpper) ||
                    r.ContactInformation.Telegram.ToUpper().Contains(promptUpper) ||
                    r.ContactInformation.Viber.ToUpper().Contains(promptUpper) ||
                    r.ContactInformation.Facebook.ToUpper().Contains(promptUpper));
            }

            var totalCount = await responses.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No responses found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedResponses = (await responses.
                OrderByDescending(a => a.CreatedAt).
                Skip((page - 1) * query.PageLength).
                Take(query.PageLength).
                ToListAsync()).Select(AnnouncementResponseWithAnnouncementDtoMapping.Func);

            var pagingInfo = new PagingInfo<AnnouncementResponseWithAnnouncementDtoWithId>(pagedResponses, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }
        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/responses/mine", PagedListMyResponsesAsync).
                Produces<PagingInfo<AnnouncementResponseDtoWithId>>().
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status404NotFound).
                Produces(StatusCodes.Status401Unauthorized).
                RequireAuthorization();
        }
    }
}
