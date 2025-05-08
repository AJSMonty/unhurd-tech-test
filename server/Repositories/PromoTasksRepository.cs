using System.Text.Json;
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

   public async Task<Result<PromoTaskListResponse>> GetTasksByAccountAsync(string accountId)
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

            return Result<PromoTaskListResponse>.Success(PromoTaskListResponse.FromEntities(results));

        }
        catch (Exception ex)
        {
            return Result<PromoTaskListResponse>.Failure(new Error("PromoTask.FetchFailed", ex.Message));
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

    public async Task<Result<PromoTask>> CreateTaskAsync(PromoTask task)
    {
        try
        {
            var response = await _container.CreateItemAsync(task, new PartitionKey(task.accountId));
            return Result<PromoTask>.Success(response.Resource);
        }
        catch (CosmosException ex)
        {
            return Result<PromoTask>.Failure(new Error("PromoTask.CreateFailed", ex.Message));
        }
        catch (Exception ex)
        {
            return Result<PromoTask>.Failure(new Error("PromoTask.UnexpectedError", ex.Message));
        }
    }

    public async Task<Result<PromoTask>> UpdateTaskAsync(string id, string accountId, PromoTask request)
    {
        try
        {
            var existingTask = await GetTaskByIdAsync(id, accountId);
            if (existingTask == null)
                return Result<PromoTask>.Failure(new Error("PromoTask.NotFound", "Task not found"));

            existingTask.title = request.title;
            existingTask.description = request.description;
            existingTask.status = request.status;
            
            var response = await _container.ReplaceItemAsync(existingTask, id, new PartitionKey(accountId));
            return Result<PromoTask>.Success(response.Resource);
        }
        catch (CosmosException ex)
        {
            return Result<PromoTask>.Failure(new Error("PromoTask.UpdateFailed", ex.Message));
        }
        catch (Exception ex)
        {
            return Result<PromoTask>.Failure(new Error("PromoTask.UnexpectedError", ex.Message));
        }
    }

   

    public async Task DeleteTaskAsync(string id, string accountId)
    {
        await _container.DeleteItemAsync<PromoTask>(id, new PartitionKey(accountId));
    }
}
