using ProductsMicroService.API.Extensions;
using ProductsMicroService.API.Middlewares;
using ProductsMicroService.BusinessLogic;
using ProductsMicroService.DataAccess;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddDataAccess(builder.Configuration)
    .AddBusinessLogic(builder.Configuration);

builder.Services.AddControllers();



builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();


builder.Services.AddProblemDetails();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
});



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerDocumentation();
    app.MapScalarDocs();
}

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

// Map your minimal API endpoints first
app.MapProductEndpoints()
   .MapCategoryEndpoints();

app.MapControllers();


await app.RunAsync();
