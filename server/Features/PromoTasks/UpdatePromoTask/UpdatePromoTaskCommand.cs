using MediatR;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.PromoTasks.UpdatePromoTask;

public sealed record UpdatePromoTaskCommand(string TaskId, UpdatePromoTaskRequest Request)
    : IRequest<Result<PromoTasksResponse>>;
