using MediatR;
using Microsoft.AspNetCore.Mvc;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.PromoTasks.CreatePromoTask;

[ApiController]
[Tags("Promo Tasks")]
public sealed class CreatePromoTaskEndpoint(ISender sender) : ControllerBase
{
    [HttpPost("promo-task")]
    public async Task<IActionResult> CreateOrUpdateAsync(
        CreatePromoTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreatePromoTaskCommand(request);

        var response = await sender.Send(command, cancellationToken);
        
        return ActionResultFactory.FromResult(response);
    }
}