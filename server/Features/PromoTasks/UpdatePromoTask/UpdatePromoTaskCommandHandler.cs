using MediatR;
using Unhurd.Api.Models;
using Unhurd.Api.Repositories;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.PromoTasks.UpdatePromoTask;

internal sealed class UpdatePromoTaskCommandHandler(
    IPromoTasksRepository promoTasksRepository)
    : IRequestHandler<UpdatePromoTaskCommand, Result<PromoTasksResponse>>
{
    public async Task<Result<PromoTasksResponse>> Handle(
        UpdatePromoTaskCommand request,
        CancellationToken cancellationToken)
    {
        var updateRequest = request.Request;

        var task = new PromoTask
        {
            id = request.TaskId,
            accountId = updateRequest.AccountId,
            title = updateRequest.Title,
            description = updateRequest.Description,
            status = updateRequest.Status,
        };

        var promoTaskResult = await promoTasksRepository.UpdateTaskAsync(request.TaskId, updateRequest.AccountId, task);

        if (promoTaskResult.IsFailure)
            return Result.Failure<PromoTasksResponse>(PromoTasksErrors.TaskNotCreated);

        return Result<PromoTasksResponse>.Success(PromoTasksResponse.FromEntity(promoTaskResult.Value!));
    }
}
