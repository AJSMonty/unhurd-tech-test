using MediatR;
using Microsoft.AspNetCore.Mvc;
using Unhurd.Infrastructure.Common;

namespace Unhurd.Api.Features.Accounts.CreateAccount;

[ApiController]
[Tags("Accounts")]
public sealed class CreateAccountEndpoint(ISender sender) : ControllerBase
{
    [HttpPost("account")]
    public async Task<IActionResult> CreateOrUpdateAsync(
        CreateAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateAccountCommand(request);

        var response = await sender.Send(command, cancellationToken);
        
        return ActionResultFactory.FromResult(response);
    }
}