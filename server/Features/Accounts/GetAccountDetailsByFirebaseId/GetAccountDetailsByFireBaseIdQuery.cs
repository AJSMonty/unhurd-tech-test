using MediatR;
using Unhurd.Api.Features.Accounts.GetAccountByFirebaseId;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.Accounts.GetAccountDetailsByFireBaseId;

public sealed record GetAccountDetailsByFireBaseIdQuery(string FirebaseId)
    : IRequest<Result<AccountResponse>>;
