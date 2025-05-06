namespace Unhurd.Api.Models;

public class Account
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string accountId { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string displayName { get; set; } = string.Empty;
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
}
