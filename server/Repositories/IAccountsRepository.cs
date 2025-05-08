using Unhurd.Api.Models;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Repositories;

public interface IAccountsRepository
{
    Task<Result<Account>> GetAccountByIdAsync(string accountId);
    Task<Result<Account>> CreateOrUpdateAsync(ILogger log, Account account);
}
