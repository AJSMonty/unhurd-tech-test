using MediatR;
using Unhurd.Api.Models;
using Unhurd.Api.Repositories;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.PromoTasks.CreatePromoTask;

internal sealed class CreatePromoTaskCommandHandler(
    IPromoTasksRepository promoTasksRepository)
    : IRequestHandler<CreatePromoTaskCommand, Result<PromoTasksResponse>>
{
    public async Task<Result<PromoTasksResponse>> Handle(
        CreatePromoTaskCommand request,
        CancellationToken cancellationToken)
    {
        var createRequest = request.Request;

        var newTask = new PromoTask
        {
            id = Guid.NewGuid().ToString(),
            title = createRequest.Title,
            description = createRequest.Description,
            accountId = createRequest.AccountId,
            status = createRequest.Status,
            createdAt = DateTime.UtcNow
        };

        var promoTaskResult = await promoTasksRepository.CreateTaskAsync(newTask);

        if (promoTaskResult.IsFailure)
            return Result.Failure<PromoTasksResponse>(PromoTasksErrors.TaskNotCreated);

        return Result<PromoTasksResponse>.Success(PromoTasksResponse.FromEntity(promoTaskResult.Value!));
    }
}
