namespace Unhurd.Api.Models;

public class PromoTask
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string AccountId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "ToDo"; // ToDo, InProgress, Done
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
