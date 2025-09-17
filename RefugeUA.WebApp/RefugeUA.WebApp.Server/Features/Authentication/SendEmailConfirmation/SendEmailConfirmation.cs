using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.Entities.Interfaces;
using RefugeUA.WebApp.Server.Features.Authentication.Login;
using RefugeUA.WebApp.Server.Features.Authentication.Register;
using RefugeUA.WebApp.Server.Options;

namespace RefugeUA.WebApp.Server.Features.Authentication.SendEmailConfirmation
{
    public class SendEmailConfirmation : IFeatureEndpoint
    {
        public static async Task<IResult> SendEmailConfirmationAsync([FromBody] SendEmailConfirmationCommand sendEmailConfirmationCommand,
            [FromServices] IValidator<SendEmailConfirmationCommand> validator,
            [FromServices] UserManager<AppUser> userManager,
            [FromServices] RoleManager<IdentityRole<long>> roleManager,
            [FromServices] IEmailSender emailSender,
            [FromServices] IHttpContextAccessor httpContext,
            [FromServices] IOptions<Frontend> frontend,
            [FromServices] IOptions<ConfirmEmailLocal> confirmEmailLocal)
        {
            var validationResult = await validator.ValidateAsync(sendEmailConfirmationCommand);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var foundUser = await userManager.FindByEmailAsync(sendEmailConfirmationCommand.Email);

            if (foundUser == null)
            {
                return Results.NotFound("User not found");
            }

            var passwordCheck = await userManager.CheckPasswordAsync(foundUser, sendEmailConfirmationCommand.Password);

            if (!passwordCheck)
            {
                return Results.BadRequest("Invalid password");
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(foundUser);
            var param = new Dictionary<string, string>()
            {
                { "token", token },
                { "email", foundUser.Email! }
            };

            var callback = frontend.Value.BaseUrl + frontend.Value.ConfirmEmailUrl + "?" +
                string.Join("&", param.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            await emailSender.SendEmailAsync(foundUser.Email!, confirmEmailLocal.Value.Header,
                $"<p>{confirmEmailLocal.Value.Body}</p> <p><a href=\"{callback}\">{confirmEmailLocal.Value.ConfirmEmailUrlLabel}</a></p>");

            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/send-email-confirmation", SendEmailConfirmationAsync)
                .WithName("SendEmailConfirmation")
                .Produces<TokenResult>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Authentication");
        }
    }
}
