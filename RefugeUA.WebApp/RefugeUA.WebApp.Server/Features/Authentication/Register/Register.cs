using FluentValidation;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.WebApp.Server.Options;

namespace RefugeUA.WebApp.Server.Features.Authentication.Register
{
    public class Register : IFeatureEndpoint
    {
        public static async Task<IResult> RegisterAsync([FromBody] RegisterCommand registerCommand,
            [FromServices] IValidator<RegisterCommand> validator,
            [FromServices] UserManager<AppUser> userManager,
            [FromServices] RoleManager<AppRole> roleManager,
            [FromServices] IEmailSender emailSender,
            [FromServices] IHttpContextAccessor httpContext,
            [FromServices] IOptions<Frontend> frontend,
            [FromServices] IOptions<ConfirmEmailLocal> confirmEmailLocal)
        {
            var validationResult = await validator.ValidateAsync(registerCommand);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var user = new AppUser()
            {
                FirstName = registerCommand.FirstName,
                LastName = registerCommand.LastName,
                Email = registerCommand.Email,
                UserName = registerCommand.Email,
                DateOfBirth = registerCommand.DateOfBirth,
                PhoneNumber = registerCommand.PhoneNumber,
                District = registerCommand.District,
            };

            var result = await userManager.CreateAsync(user, registerCommand.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.
                    ToDictionary(e => e.Code, e => new List<string>() { e.Description }) as IDictionary<string, string[]>;

                return Results.ValidationProblem(errors!);
            }

            await userManager.AddToRoleAsync(user, registerCommand.Role);

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string>()
            {
                { "token", token },
                { "email", user.Email }
            };

            var callback = frontend.Value.BaseUrl + frontend.Value.ConfirmEmailUrl + "?" + 
                string.Join("&", param.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            await emailSender.SendEmailAsync(user.Email, confirmEmailLocal.Value.Header,
                $"<p>{confirmEmailLocal.Value.Body}</p> <p><a href=\"{callback}\"></a></p>");

            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/register", RegisterAsync)
                .WithName("Register")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
                .WithTags("Authentication");
        }
    }
}
