
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common;
using RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.PagedList
{
    public class PagedPsychologistInformationsList : IFeatureEndpoint
    {
        public static async Task<IResult> PagedPsychologistInformationsListAsync(
            [AsParameters] PagedPsychologistInformationsListQuery query,
            [FromServices] IValidator<PagedPsychologistInformationsListQuery> validator,
            [FromServices] RefugeUADbContext dbContext)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var psychologistInformations = dbContext.PsychologistInformation.AsNoTracking().
                Include(p => p.Author).
                Include(p => p.Contact).
                AsQueryable();

            if (query.Prompt != null)
            {
                var queryPrompt = query.Prompt.ToUpper();
                psychologistInformations = psychologistInformations.Where(a => a.Title.ToUpper().Contains(queryPrompt));
            }

            var totalCount = await psychologistInformations.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No psychologist informations found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedPychologistInformations = await psychologistInformations.
                OrderByDescending(a => a.CreatedAt).
                Select(PsychologistInformationResultMapping.Expression).
                Skip((page - 1) * query.PageLength).
                Take(query.PageLength).
                ToListAsync();

            var pagingInfo = new PagingInfo<PsychologistInformationResult>(pagedPychologistInformations, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/mental-support/psychologist-informations/paged", PagedPsychologistInformationsListAsync)
                .Produces<PagingInfo<PsychologistInformationResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("PagedPsychologistInformations")
                .WithTags("MentalSUpport");
        }
    }
}
