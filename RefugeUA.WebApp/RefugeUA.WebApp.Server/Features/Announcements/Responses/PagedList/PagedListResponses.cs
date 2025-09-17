using Azure;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Announcements.Common;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;
using RefugeUA.WebApp.Server.Features.Announcements.Responses.Common;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;
using System.Linq.Expressions;

namespace RefugeUA.WebApp.Server.Features.Announcements.Responses.PagedList
{
    public class PagedListResponses : IFeatureEndpoint
    {
        public static async Task<IResult> PagedListResponsesAsync(
            [FromRoute] long id,
            [AsParameters] PagedListResponsesQuery query,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IAuthorizationService authService,
            [FromServices] IValidator<PagedListResponsesQuery> validator,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var foundAnnouncement = await dbContext.Announcements.FirstOrDefaultAsync(a => a.Id == id);
            
            if (foundAnnouncement == null)
            {
                return Results.NotFound();
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                BaseAnnouncementResultMapping.Func(foundAnnouncement),
                Policies.EditDeleteAnnouncementPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            var responses = dbContext.AnnouncementResponses.Where(r => r.AnnouncementId == id);

            if(query.Prompt != null)
            {
                var promptUpper = query.Prompt.ToUpper();
                responses = responses.Where(r => r.User.FirstName.ToUpper().Contains(promptUpper) ||
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

            var pagedResponses = await responses.
                OrderByDescending(a => a.CreatedAt).
                Select(AnnouncementResponseDtoMapping.Expression).
                Skip((page - 1) * query.PageLength).
                Take(query.PageLength).
                ToListAsync();

            var pagingInfo = new PagingInfo<AnnouncementResponseDtoWithId>(pagedResponses, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/{id:long}/responses", PagedListResponsesAsync).
                Produces<PagingInfo<AnnouncementResponseDtoWithId>>().
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status404NotFound).
                Produces(StatusCodes.Status403Forbidden).
                Produces(StatusCodes.Status401Unauthorized).
                RequireAuthorization();
        }
    }
}
