using Unhurd.Api.Models;

namespace Unhurd.Api.Features.Accounts.GetAccountByFirebaseId;

public sealed record AccountResponse(string AccountId, string Email, string DisplayName, DateTime CreatedAt, string Id)
{
    public static AccountResponse FromEntity(Account account) =>
        new(account.accountId, account.email, account.displayName, account.createdAt, account.id);
}
