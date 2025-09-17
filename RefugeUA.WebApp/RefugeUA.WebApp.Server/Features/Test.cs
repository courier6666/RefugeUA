using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Shared.Converters;
using System.Text.Json.Serialization;

namespace RefugeUA.WebApp.Server.Features
{
    public class Test : IFeatureEndpoint
    {
        public class TestBody
        {
            [JsonConverter(typeof(MillisecondsTimeSpanConverter))]
            public TimeSpan Span { get; set; }
        }

        public static async Task<IResult> TestAsync([FromServices] RefugeUADbContext dbContext)
        {
            var announcement = await dbContext.Announcements
                .Include(x => x.Address)
                .Include(x => x.ContactInformation)
                .Include(x => x.Responses)
                .FirstOrDefaultAsync(x => x.Id == 1);

            Type t = announcement.GetType();

            return Results.Ok();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/test", TestAsync);
        }
    }
}
