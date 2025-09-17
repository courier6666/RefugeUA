using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.WebApp.Server.Services;

namespace RefugeUA.WebApp.Server.Features.Authentication.Login
{
    public class Login : IFeatureEndpoint
    {
        public static async Task<IResult> LoginAsync(
            [FromBody] LoginCommand loginCommand,
            [FromServices] IValidator<LoginCommand> validator,
            [FromServices] UserManager<AppUser> userManager,
            [FromServices] JwtHandler jwtHandler)
        {
            var validationResult = validator.Validate(loginCommand);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var foundUser = await userManager.FindByEmailAsync(loginCommand.Email);

            if (foundUser == null)
            {
                return Results.NotFound("User not found");
            }

            if (!foundUser.EmailConfirmed)
            {
                return Results.Problem("Not confirmed: Email is not confirmed!", statusCode: StatusCodes.Status403Forbidden);
            }

            if (!foundUser.IsAccepted)
            {
                return Results.Problem("Not accepted: User registration not accepted by admin!", statusCode: StatusCodes.Status403Forbidden);
            }

            var passwordCheck = await userManager.CheckPasswordAsync(foundUser, loginCommand.Password);

            if (!passwordCheck)
            {
                return Results.BadRequest("Invalid password");
            }

            var token = await jwtHandler.CreateTokenAsync(foundUser);

            return Results.Ok(new TokenResult()
            {
                Token = token,
            });
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/login", LoginAsync)
                .WithName("Login")
                .Produces<TokenResult>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Authentication");
        }
    }
}
