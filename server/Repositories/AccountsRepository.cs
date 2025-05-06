using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Unhurd.Api.Models;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Repositories;

public class AccountsRepository : IAccountsRepository
{
    private readonly Container _container;

    public AccountsRepository(CosmosClient client)
    {
        _container = client.GetContainer("unhurd-tech-test", "accounts");
    }

    public async Task<Result<Account>> GetAccountByIdAsync(string accountId)
    {
        try
        {
            var queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.accountId = @accountId")
                                    .WithParameter("@accountId", accountId);

            var queryIterator = _container.GetItemQueryIterator<Account>(
                queryDefinition,
                requestOptions: new QueryRequestOptions
                {
                    PartitionKey = new PartitionKey(accountId),
                    MaxItemCount = 1
                });

            if (queryIterator.HasMoreResults)
            {
                var response = await queryIterator.ReadNextAsync();
                var account = response.FirstOrDefault();

                if (account != null)
                    return Result<Account>.Success(account);
            }

            return Result<Account>.Failure(AccountErrors.NotFound);
        }
        catch (Exception ex)
        {
            return Result<Account>.Failure(new Error("Account.UnexpectedError", ex.Message));
        }
    }

    public async Task<Result<Account>> CreateOrUpdateAsync(ILogger log, Account account)
    {
        try
        {
            var response = await _container.UpsertItemAsync(account, new PartitionKey(account.accountId));

            return Result<Account>.Success(response.Resource);
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Failed to upsert account");
            return Result<Account>.Failure(new Error("Account.UpsertFailed", ex.Message));
        }
    }
}
