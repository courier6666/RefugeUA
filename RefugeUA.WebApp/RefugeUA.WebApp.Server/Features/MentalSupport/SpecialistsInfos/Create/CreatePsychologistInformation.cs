
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Common;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Create
{
    public class CreatePsychologistInformation : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin,CommunityAdmin")]
        public static async Task<IResult> CreatePsychologistInformationAsync(
            [FromBody] EditOrCreatePsychologistInformationCommand command,
            [FromServices] IValidator<EditOrCreatePsychologistInformationCommand> validator,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var userId = httpContextAccessor.HttpContext!.User.GetId() ?? 0;

            var psychologistInformation = new PsychologistInformation()
            {
                Title = command.Title,
                Description = command.Description,
                Contact = new ContactInformation()
                {
                    PhoneNumber = command.ContactInformation.PhoneNumber,
                    Email = command.ContactInformation.Email,
                    Telegram = command.ContactInformation.Telegram,
                    Viber = command.ContactInformation.Viber,
                    Facebook = command.ContactInformation.Facebook,
                },
                AuthorId = userId,
            };

            dbContext.PsychologistInformation.Add(psychologistInformation);
            await dbContext.SaveChangesAsync();
            return Results.Ok(psychologistInformation.Id);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/mental-support/psychologist-informations", CreatePsychologistInformationAsync).
                Produces<long>(StatusCodes.Status200OK).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("MentalSupport").
                WithName("CreatePsychologistInformation").
                RequireAuthorization();
        }
    }
}
