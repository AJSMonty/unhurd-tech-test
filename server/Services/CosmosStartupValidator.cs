using Microsoft.Azure.Cosmos;

public class CosmosStartupValidator : IHostedService
{
    private readonly CosmosClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger<CosmosStartupValidator> _logger;

    public CosmosStartupValidator(CosmosClient client, IConfiguration config, ILogger<CosmosStartupValidator> logger)
    {
        _client = client;
        _config = config;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var dbName = _config.GetSection("CosmosDb")["DatabaseName"];
        try
        {
            var db = _client.GetDatabase(dbName);

            await db.GetContainer("accounts").ReadContainerAsync(null, cancellationToken);
            await db.GetContainer("promo-tasks").ReadContainerAsync(null, cancellationToken);

            _logger.LogInformation("✅ Cosmos DB containers validated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Cosmos DB validation failed.");
            throw; // Prevent app from starting if critical
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
