using MediatR;
using Unhurd.Api.Features.Accounts.GetAccountByFirebaseId;
using Unhurd.Api.Models;
using Unhurd.Api.Repositories;
using Unhurd.Infrastructure.Common;


namespace Unhurd.Api.Features.Accounts.CreateAccount;

internal sealed class CreateAccountCommandHandler(
    ILogger<CreateAccountCommandHandler> log,
    IAccountsRepository accountsRepository)
    : IRequestHandler<CreateAccountCommand, Result<AccountResponse>>
{
    public async Task<Result<AccountResponse>> Handle(
        CreateAccountCommand request,
        CancellationToken cancellationToken)
    {
        var createRequest = request.Request;
        var newAccount = new Account
        {
            id = Guid.NewGuid().ToString(),
            accountId = createRequest.AccountId,
            displayName = createRequest.DisplayName ?? string.Empty,
            email = createRequest.Email,
            createdAt = DateTime.UtcNow,
        };

        var accountResult = await accountsRepository.CreateOrUpdateAsync(log, newAccount);

        if (accountResult.IsFailure)
            return Result.Failure<AccountResponse>(AccountErrors.AccountNotCreated);

        return Result<AccountResponse>.Success(AccountResponse.FromEntity(accountResult.Value!));
    }
}
