using Unhurd.Api.Models;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Repositories;

public interface IPromoTasksRepository
{
    Task<Result<PromoTaskListResponse>> GetTasksByAccountAsync(string accountId);
    Task<PromoTask?> GetTaskByIdAsync(string id, string accountId);
    Task<Result<PromoTask>> CreateTaskAsync(PromoTask task);

    Task<Result<PromoTask>> UpdateTaskAsync(string taskId, string accountId, PromoTask task);
    Task DeleteTaskAsync(string id, string accountId);
}
