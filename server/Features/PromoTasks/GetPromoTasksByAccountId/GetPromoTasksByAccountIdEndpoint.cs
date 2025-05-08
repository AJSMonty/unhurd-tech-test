using MediatR;
using Microsoft.AspNetCore.Mvc;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.PromoTasks.GetPromoTasksByAccountId;

[ApiController]
[Tags("Promo Tasks")]
public sealed class GetPromoTasksByAccountIdEndpoint(ISender sender) : ControllerBase
{
    [HttpGet("promo-tasks/{accountId}")]
    public async Task<IActionResult> GetPromoTasksByAccountIdAsync(
        string accountId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPromoTasksByAccountIdQuery(accountId);
        
        var result = await sender.Send(query, cancellationToken);
        
        return ActionResultFactory.FromResult(result);
    }
}