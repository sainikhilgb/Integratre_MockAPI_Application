using System;
using System.Net;
using Integrate_MockAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Integrate_MockAPI.Helpers;

public class ApiResponseHandler
{
    public static IActionResult HandleApiError(HttpResponseMessage response, string? customMessage = null)
        {
            var statusCode = (int)response.StatusCode;

            var errorResponse = new ApiErrorResponse
            {
                StatusCode = statusCode,
                Error = response.ReasonPhrase ?? "Unknown Error",
                Message = customMessage ?? $"External API returned status code {statusCode}.",
                Timestamp = DateTime.UtcNow
            };

            return statusCode switch
            {
                (int)HttpStatusCode.NotFound => new NotFoundObjectResult(errorResponse),
                (int)HttpStatusCode.BadRequest => new BadRequestObjectResult(errorResponse),
                _ => new ObjectResult(errorResponse)
                {
                    StatusCode = 502
                }
            };
        }

}
