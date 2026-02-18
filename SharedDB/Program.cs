using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSwag.AspNetCore;
using SharedDB;
using SharedDB.Context;

//TODO: Configurar o Swagger para aceitar o token JWT e proteger as rotas com autorização
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthorization();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//builder.Services.AddOpenApi(options =>
//{
//    options.AddDocumentTransformer((document, context, cancellationToken) =>
//    {
//        document.Components ??= new Microsoft.OpenApi.OpenApiComponents();
//        document.Components.SecuritySchemes?.Add("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
//        {
//            Type = Microsoft.OpenApi.SecuritySchemeType.Http,
//            Scheme = "bearer",
//            BearerFormat = "JWT",
//            In = Microsoft.OpenApi.ParameterLocation.Header,
//            Description = "Insira o token JWT no formato: Bearer {seu_token}"
//        });

//        document.Security?.Add(new Microsoft.OpenApi.OpenApiSecurityRequirement
//        {
//            {
//                new Microsoft.OpenApi.OpenApiSecurityScheme
//                {
//                    Reference = new Microsoft.OpenApi.OpenApiReference
//                    {
//                        Type = Microsoft.OpenApi.ReferenceType.SecurityScheme,
//                        Id = "Bearer"
//                    }
//                },
//                Array.Empty<string>()
//            }
//        });
//        return Task.CompletedTask;
//    });
//});
//builder.Services.AddDbContext<ApplicationDbContext>(
//    options => options.UseInMemoryDatabase("AppDb"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite(connectionString == null ? "users.db" : connectionString));

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddOpenApiDocument(config =>
//{
//    config.DocumentName = "SharedDB";
//    config.Title = "SDB v1";
//    config.Version = "v1";
//});

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
//});

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddAuthorization(options =>
//{

//    options.AddPolicy("Admin",
//        authBuilder =>
//        {
//            authBuilder.RequireRole("Administrators");
//        });

//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.MapIdentityApi<IdentityUser>();

app.UseSwaggerUi(config =>
{
    config.DocumentTitle = "Shared DB";
    config.Path = "/swagger";
    config.DocumentPath = "/openapi/v1.json";
    config.DocExpansion = "list";
});

//Routes

app.MapGet("/wthr", (HttpContext httpContext) =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = WeatherForecast.Summaries[Random.Shared.Next(WeatherForecast.Summaries.Length)]
        })
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");
//.RequireAuthorization("Admin");

//app.MapSwagger().RequireAuthorization();

app.Run();
