using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RefugeUA.WebApp.Server.Swagger.Schemas
{
    public class TimeSpanSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(TimeSpan))
            {
                schema.Type = "integer";
                schema.Format = "int64";
                schema.Example = new Microsoft.OpenApi.Any.OpenApiLong(0);
                schema.Properties.Clear();
            }
        }
    }
}
