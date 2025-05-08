namespace Unhurd.Api.Models;

public class PromoTask
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string accountId { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;

    public string description { get; set; } = string.Empty;
    public string status { get; set; } = "ToDo"; // ToDo, InProgress, Done
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
}
