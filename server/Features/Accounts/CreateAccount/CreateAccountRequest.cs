public sealed record CreateAccountRequest(
    string AccountId,
    string Email,
    string? DisplayName);