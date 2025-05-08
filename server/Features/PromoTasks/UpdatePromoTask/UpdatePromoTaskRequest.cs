public sealed record UpdatePromoTaskRequest(
    string Id,
    string AccountId,
    string Title,
    string Description,
    string Status);