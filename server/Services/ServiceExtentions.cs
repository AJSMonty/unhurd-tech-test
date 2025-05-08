using Microsoft.Azure.Cosmos;
using Unhurd.Api.Configurations;
using Unhurd.Api.Repositories;

namespace Unhurd.Api.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
    {
        var cosmosSettings = config.GetSection("CosmosDb");
        services.Configure<CosmosDbConfig>(cosmosSettings);

        var account = cosmosSettings["Account"];
        var key = cosmosSettings["Key"];
        var dbName = cosmosSettings["DatabaseName"];

        CosmosClient client = new(account, key);
        services.AddSingleton(client);

        var db = client.GetDatabase(dbName);
        services.AddSingleton(db.GetContainer("accounts"));
        services.AddSingleton(db.GetContainer("promo-tasks"));

        services.AddScoped<IAccountsRepository, AccountsRepository>();
        services.AddScoped<IPromoTasksRepository, PromoTasksRepository>();

        return services;
    }
}
