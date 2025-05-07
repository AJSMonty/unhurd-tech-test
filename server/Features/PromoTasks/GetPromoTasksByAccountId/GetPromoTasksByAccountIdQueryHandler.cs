using MediatR;
using Unhurd.Api.Repositories;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.PromoTasks.GetPromoTasksByAccountId;

internal sealed class GetPromoTasksByAccountIdQueryHandler(
    IPromoTasksRepository promoTasksRepository)
    : IRequestHandler<GetPromoTasksByAccountIdQuery, Result<PromoTasksResponse>>
{
    public async Task<Result<PromoTasksResponse>> Handle(
        GetPromoTasksByAccountIdQuery request,
        CancellationToken cancellationToken)
    {
        var tasksResult = await promoTasksRepository.GetTasksByAccountAsync(request.accountId);

        if (tasksResult.IsFailure || tasksResult.Value is null || !tasksResult.Value.Any())
            return Result.Failure<PromoTasksResponse>(PromoTasksErrors.NotFound);

        return Result<PromoTasksResponse>.Success(PromoTasksResponse.FromList(tasksResult.Value));
    }
}
