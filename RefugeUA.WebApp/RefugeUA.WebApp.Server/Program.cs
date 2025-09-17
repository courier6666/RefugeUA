using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using RefugeUA.DatabaseAccess;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.WebApp.Server.Extensions;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using RefugeUA.WebApp.Server.Middleware;
using RefugeUA.WebApp.Server.Services;
using RefugeUA.WebApp.Server.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using RefugeUA.WebApp.Server.Data;
using RefugeUA.WebApp.Server.Extensions.Authorization;
using RefugeUA.WebApp.Server.Shared.Converters;
using RefugeUA.WebApp.Server.Swagger.Schemas;
using RefugeUA.FileManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<Frontend>(builder.Configuration.GetSection("Frontend"));
builder.Services.Configure<ConfirmEmailLocal>(builder.Configuration.GetSection("ConfirmEmailLocal"));

var connectionString = builder.Configuration.GetConnectionString("RefugeUa");

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.Converters.Add(new MillisecondsTimeSpanConverter());
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
            },
            Array.Empty<string>()
        },
    });
});

builder.Services.AddRefugeUaAuthorization();
builder.Services.AddAntiforgery();
builder.Services.AddValidators();
builder.Services.AddDbContext<RefugeUADbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("RefugeUA.WebApp.Server")));

builder.Services.AddIdentity<AppUser, AppRole>().
    AddEntityFrameworkStores<RefugeUADbContext>().
    AddDefaultTokenProviders();

builder.Services.AddScoped<JwtHandler>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IFileManager, FileManager>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtTokenServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<TimeSpanSchemaFilter>();
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseMiddleware<ExceptionHandlingMiddleware>();
}

app.UseRouting();

app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapAllEndpointsFromCurrentAssembly();

app.MapFallbackToFile("index.html");

await SeedRoles.SeedRolesAsync(app);

await app.RunAsync();
