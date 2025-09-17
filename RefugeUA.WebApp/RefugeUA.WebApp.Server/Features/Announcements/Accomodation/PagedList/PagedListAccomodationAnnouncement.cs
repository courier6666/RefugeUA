using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;
using static RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common.ListAccomodationAnnouncementMapping;
using RefugeUA.Entities;

namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.PagedList
{
    public class PagedListAccomodationAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> PagedListAccomodationAnnouncementAsync(
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IValidator<PagedListAccomodationAnnouncementQuery> validator,
            [AsParameters] PagedListAccomodationAnnouncementQuery query)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var accomodationAnnouncements = dbContext.AccomodationAnnouncements
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
                accomodationAnnouncements = accomodationAnnouncements.
                    Where(a => a.Title.ToUpper().Contains(promptUpper));
            }

            if (query.IsClosed != null)
            {
                accomodationAnnouncements = accomodationAnnouncements.Where(a => a.IsClosed == query.IsClosed);
            }

            if (query.District != null)
            {
                accomodationAnnouncements = accomodationAnnouncements.Where(a => a.Address.District.ToUpper().Contains(query.District.ToUpper()));
            }

            if (query.AnnouncementGroup != null)
            {
                accomodationAnnouncements = accomodationAnnouncements
                    .Where(a => a.Groups.Any(g => g.Name.ToUpper().Contains(query.AnnouncementGroup.ToUpper())));
            }

            if (query.IsFree == null || !(query.IsFree ?? true))
            {
                if (query.PriceLower != null)
                {
                    accomodationAnnouncements = accomodationAnnouncements
                        .Where(a => a.Price != null && (a.Price >= query.PriceLower));
                }

                if (query.PriceUpper != null)
                {
                    accomodationAnnouncements = accomodationAnnouncements
                        .Where(a => a.Price != null && (a.Price <= query.PriceUpper));
                }
            }
            else
            {
                accomodationAnnouncements = accomodationAnnouncements
                    .Where(a => a.Price == null);
            }

            if (query.AreaSqMetersLower != null)
            {
                accomodationAnnouncements = accomodationAnnouncements
                    .Where(a => a.AreaSqMeters != null && (a.AreaSqMeters >= query.AreaSqMetersLower));
            }

            if (query.AreaSqMetersUpper != null)
            {
                accomodationAnnouncements = accomodationAnnouncements
                    .Where(a => a.AreaSqMeters != null && (a.AreaSqMeters <= query.AreaSqMetersUpper));
            }

            if (query.Capacity != null)
            {
                accomodationAnnouncements = accomodationAnnouncements
                    .Where(a => a.Capacity >= query.Capacity);
            }

            if (query.Floors != null)
            {
                accomodationAnnouncements = accomodationAnnouncements
                    .Where(a => a.Floors >= query.Floors);
            }

            if (query.NumberOfRooms != null)
            {
                accomodationAnnouncements = accomodationAnnouncements
                    .Where(a => a.NumberOfRooms >= query.NumberOfRooms);
            }

            if (query.PetsAllowed != null)
            {
                accomodationAnnouncements = accomodationAnnouncements
                .Where(a => a.PetsAllowed == query.PetsAllowed);
            }

            if (query.BuildingTypes != null && query.BuildingTypes.Length > 0)
            {
                accomodationAnnouncements = accomodationAnnouncements
                    .Where(a => query.BuildingTypes.Contains(a.BuildingType));
            }

            var totalCount = await accomodationAnnouncements.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No accomodation announcements found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedAccomodationAnnouncements = await accomodationAnnouncements.
                OrderByDescending(a => a.CreatedAt).
                Select(Expression).
                Skip((page - 1) * query.PageLength).
                Take(query.PageLength).
                ToListAsync();

            var pagingInfo = new PagingInfo<AccomodationAnnouncementResult>(pagedAccomodationAnnouncements, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }
        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/accomodation/paged", PagedListAccomodationAnnouncementAsync)
                .Produces<PagingInfo<AccomodationAnnouncementResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("PagedListAccomodationAnnouncement")
                .WithTags("AccomodationAnnouncements");
        }
    }
}
