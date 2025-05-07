using Unhurd.Api.Models;

public sealed record PromoTasksResponse(List<PromoTask> PromoTasks)
{
    public static PromoTasksResponse FromList(List<PromoTask> tasks) =>
        new(tasks);
}
