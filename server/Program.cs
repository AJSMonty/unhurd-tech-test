using Microsoft.OpenApi.Models;
using Unhurd.Api.Middleware;
using Unhurd.Api.Services;
using Unhurd.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddHostedService<CosmosStartupValidator>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Unhurd API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
// app.UseHttpsRedirection();

app.MapControllers();

app.Run();
