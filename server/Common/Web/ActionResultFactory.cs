using Microsoft.AspNetCore.Mvc;

namespace Unhurd.Infrastructure.Common;

public static class ActionResultFactory
{
    public static IActionResult FromResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return new ObjectResult(result.Error)
        {
            StatusCode = StatusCodeFromError(result.Error)
        };
    }

    private static int StatusCodeFromError(Error error)
    {
        // Map known error codes to HTTP status codes if needed
        return error.Code switch
        {
            "Error.NotFound" => 404,
            "Error.Unauthorized" => 401,
            "Error.Validation" => 400,
            _ => 500
        };
    }
}
