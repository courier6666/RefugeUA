
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common;
using static RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common.ListAccomodationAnnouncementMapping;

namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.List
{
    public class ListAccomodationAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> ListAccomodationAnnouncementAsync(
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IValidator<ListAccomodationAnnouncementQuery> validator,
            [AsParameters] ListAccomodationAnnouncementQuery query)
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

            var result = await accomodationAnnouncements.
                OrderByDescending(a => a.CreatedAt).
                Select(Expression).
                ToListAsync();

            if (result.Count == 0)
            {
                return Results.NotFound("No accomodation announcements found with such parameters!");
            }

            return Results.Ok(result);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/accomodation", ListAccomodationAnnouncementAsync)
                .Produces<List<AccomodationAnnouncementResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("ListAccomodationAnnouncement")
                .WithTags("AccomodationAnnouncements");
        }
    }
}
