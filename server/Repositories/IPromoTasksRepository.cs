using Unhurd.Api.Models;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Repositories;

public interface IPromoTasksRepository
{
    Task<Result<List<PromoTask>>> GetTasksByAccountAsync(string accountId);
    Task<PromoTask?> GetTaskByIdAsync(string id, string accountId);
    Task CreateOrUpdateTaskAsync(PromoTask task);
    Task DeleteTaskAsync(string id, string accountId);
}
