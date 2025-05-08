using MediatR;
using Unhurd.Api.Features.Accounts.GetAccountByFirebaseId;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.Accounts.CreateAccount;

public sealed record CreateAccountCommand(CreateAccountRequest Request)
    : IRequest<Result<AccountResponse>>;
