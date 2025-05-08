using MediatR;
using Unhurd.Api.Repositories;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.PromoTasks.GetPromoTasksByAccountId;

internal sealed record class GetPromoTasksByAccountIdQueryHandler(
    IPromoTasksRepository promoTasksRepository)
    : IRequestHandler<GetPromoTasksByAccountIdQuery, Result<PromoTaskListResponse>>
{
    public async Task<Result<PromoTaskListResponse>> Handle(
        GetPromoTasksByAccountIdQuery request,
        CancellationToken cancellationToken)
    {
        var tasksResult = await promoTasksRepository.GetTasksByAccountAsync(request.accountId);

        if (tasksResult.IsFailure || tasksResult.Value is null || !tasksResult.Value.Tasks.Any())
            return Result.Failure<PromoTaskListResponse>(PromoTasksErrors.NotFound);

        return tasksResult;
    }
}
