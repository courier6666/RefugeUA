
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common;
using RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Common;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Detail
{
    public class DetailPsychologistInformation : IFeatureEndpoint
    {
        public static async Task<IResult> DetailPsychologistInformationAsync([FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext)
        {
            var foundInfo = await dbContext.PsychologistInformation.AsNoTracking().
                Include(p => p.Contact).
                Include(p => p.Author).
                Select(PsychologistInformationResultMapping.Expression).
                FirstOrDefaultAsync(p => p.Id == id);

            if (foundInfo == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(foundInfo);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/mental-support/psychologist-informations/{id:long}", DetailPsychologistInformationAsync)
                .WithName("GetPsychologistInformation")
                .Produces<PsychologistInformationResult>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("MentalSupport");
        }
    }
}
