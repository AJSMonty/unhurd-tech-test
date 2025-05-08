using MediatR;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.PromoTasks.CreatePromoTask;

public sealed record CreatePromoTaskCommand(CreatePromoTaskRequest Request)
    : IRequest<Result<PromoTasksResponse>>;
