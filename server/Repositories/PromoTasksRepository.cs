using Microsoft.Azure.Cosmos;
using Unhurd.Api.Models;

namespace Unhurd.Api.Repositories;

public class PromoTasksRepository : IPromoTasksRepository
{
    private readonly Container _container;

    public PromoTasksRepository(CosmosClient client)
    {
        _container = client.GetContainer("unhurd-tech-test", "promo-tasks");
    }

    public async Task<List<PromoTask>> GetTasksByAccountAsync(string accountId)
    {
        var query = new QueryDefinition("SELECT * FROM c WHERE c.accountId = @accountId")
            .WithParameter("@accountId", accountId);

        var resultSet = _container.GetItemQueryIterator<PromoTask>(query);
        var results = new List<PromoTask>();
        while (resultSet.HasMoreResults)
        {
            var response = await resultSet.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public async Task<PromoTask?> GetTaskByIdAsync(string id, string accountId)
    {
        try
        {
            var response = await _container.ReadItemAsync<PromoTask>(id, new PartitionKey(accountId));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task CreateOrUpdateTaskAsync(PromoTask task)
    {
        await _container.UpsertItemAsync(task, new PartitionKey(task.AccountId));
    }

    public async Task DeleteTaskAsync(string id, string accountId)
    {
        await _container.DeleteItemAsync<PromoTask>(id, new PartitionKey(accountId));
    }
}
