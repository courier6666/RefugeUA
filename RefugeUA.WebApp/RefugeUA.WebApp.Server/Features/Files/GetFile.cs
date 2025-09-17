
using Microsoft.AspNetCore.Mvc;
using RefugeUA.FileManager;

namespace RefugeUA.WebApp.Server.Features.Files
{
    public class GetFile : IFeatureEndpoint
    {
        public static async Task<IResult> GetFileAsync([FromServices] IFileManager fileManager, [FromRoute] string filename)
        {
            var res = await fileManager.GetFileAsync("images", filename);

            if (res == null)
            {
                return Results.NotFound();
            }

            return Results.File(res, "image/jpeg");
        }
        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/images/{filename}", GetFileAsync).
                Produces<byte[]>(StatusCodes.Status200OK).
                Produces(StatusCodes.Status404NotFound);
        }
    }
}
