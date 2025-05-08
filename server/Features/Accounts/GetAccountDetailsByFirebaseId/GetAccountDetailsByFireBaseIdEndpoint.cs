using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.Accounts.GetAccountDetailsByFireBaseId;

[ApiController]
[Authorize]
[Tags("Accounts")]
public sealed class GetAccountDetailsByFireBaseIdEndpoint(ISender sender) : ControllerBase
{
    [HttpGet("account/{accountId}")]
    public async Task<IActionResult> GetAccountDetailsByFireBaseIdAsync(
        string accountId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAccountDetailsByFireBaseIdQuery(accountId);
        
        var result = await sender.Send(query, cancellationToken);
        
        return ActionResultFactory.FromResult(result);
    }
}