using MediatR;
using Unhurd.Api.Features.Accounts.GetAccountDetailsByFireBaseId;
using Unhurd.Api.Repositories;
using Unhurd.Infrastructure.Common;


namespace Unhurd.Api.Features.Accounts.GetAccountByFirebaseId;

internal sealed class GetAccountByFirebaseIdQueryHandler(
    IAccountsRepository accountsRepository)
    : IRequestHandler<GetAccountDetailsByFireBaseIdQuery, Result<AccountResponse>>
{
    public async Task<Result<AccountResponse>> Handle(
        GetAccountDetailsByFireBaseIdQuery request,
        CancellationToken cancellationToken)
    {
        var accountResult = await accountsRepository.GetAccountByIdAsync(request.FirebaseId);

        if (accountResult.IsFailure)
            return Result.Failure<AccountResponse>(AccountErrors.NotFound);

        return Result<AccountResponse>.Success(AccountResponse.FromEntity(accountResult.Value!));
    }
}
