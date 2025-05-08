using Unhurd.Api.Models;

public sealed record PromoTasksResponse(string Id, string Title, string Description, string AccountId, string Status, DateTime CreatedAt)
{
    public static PromoTasksResponse FromEntity(PromoTask task) =>
        new(task.id, task.title, task.description, task.accountId, task.status, task.createdAt);
}

public sealed class PromoTaskListResponse
{
    public List<PromoTasksResponse> Tasks { get; init; } = [];

    public static PromoTaskListResponse FromEntities(List<PromoTask> tasks) =>
        new PromoTaskListResponse
        {
            Tasks = tasks.Select(PromoTasksResponse.FromEntity).ToList()
        };
}
