using Microsoft.Azure.Cosmos;
using Unhurd.Api.Models;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Repositories;

public class PromoTasksRepository : IPromoTasksRepository
{
    private readonly Container _container;

    public PromoTasksRepository(CosmosClient client)
    {
        _container = client.GetContainer("unhurd-tech-test", "promo-tasks");
    }

   public async Task<Result<List<PromoTask>>> GetTasksByAccountAsync(string accountId)
{
    try
    {
        var query = new QueryDefinition("SELECT * FROM c WHERE c.accountId = @accountId")
            .WithParameter("@accountId", accountId);

        var resultSet = _container.GetItemQueryIterator<PromoTask>(
            query,
            requestOptions: new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(accountId)
            });

        var results = new List<PromoTask>();

        while (resultSet.HasMoreResults)
        {
            var response = await resultSet.ReadNextAsync();
            results.AddRange(response);
        }

        return Result<List<PromoTask>>.Success(results);
    }
    catch (Exception ex)
    {
        return Result<List<PromoTask>>.Failure(new Error("PromoTask.FetchFailed", ex.Message));
    }
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
