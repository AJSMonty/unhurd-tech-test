using MediatR;
using Microsoft.AspNetCore.Mvc;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.PromoTasks.UpdatePromoTask;

[ApiController]
[Tags("Promo Tasks")]
public sealed class UpdatePromoTaskEndpoint(ISender sender) : ControllerBase
{
    [HttpPut("promo-task/{taskId}")]
    public async Task<IActionResult> UpdateTaskAsync(
        string taskId,
        UpdatePromoTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdatePromoTaskCommand(taskId, request);

        var response = await sender.Send(command, cancellationToken);
        
        return ActionResultFactory.FromResult(response);
    }
}