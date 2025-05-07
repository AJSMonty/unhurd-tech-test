using MediatR;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.PromoTasks.GetPromoTasksByAccountId;

public sealed record GetPromoTasksByAccountIdQuery(string accountId)
    : IRequest<Result<PromoTaskListResponse>>;
