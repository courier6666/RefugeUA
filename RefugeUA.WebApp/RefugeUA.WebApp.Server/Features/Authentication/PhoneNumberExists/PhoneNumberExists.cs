
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess.Identity;

namespace RefugeUA.WebApp.Server.Features.Authentication.PhoneNumberExists
{
    public class PhoneNumberExists : IFeatureEndpoint
    {
        public static async Task<IResult> PhoneNumberExistsAsync(
            [FromQuery] string phoneNumber,
            [FromServices] UserManager<AppUser> userManager)
        {
            var foundUser = await userManager.Users
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            return foundUser != null
                ? Results.Ok(true)
                : Results.Ok(false);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/auth/phonenumber-exists", PhoneNumberExistsAsync)
                .Produces(StatusCodes.Status200OK)
                .Produces<bool>()
                .WithName("PhoneNumberExists")
                .WithTags("Authentication");
        }
    }
}
