public sealed record CreatePromoTaskRequest(
    string AccountId,
    string Title,
    string Description,
    string Status);