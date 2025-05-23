using Microsoft.AspNetCore.DataProtection;
using Azure.Identity;
using Microsoft.OpenApi.Models;
using Unhurd.Api.Middleware;
using Unhurd.Api.Services;
using Unhurd.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var keyVaultName = builder.Configuration["KeyVaultName"];
if (!string.IsNullOrEmpty(keyVaultName))
{
    var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
    builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());
}

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddFirebaseAuthentication(builder.Configuration);
builder.Services.AddHostedService<CosmosStartupValidator>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Unhurd API", Version = "v1" });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendDev", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://wonderful-meadow-02d574f03.6.azurestaticapps.net")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "keys")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontendDev");

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
