using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace MetaFinance.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return result.Value is not null
                ? TypedResults.Ok(result.Value)
                : TypedResults.NoContent();
        }

        return BadRequest(result);
    }

    public static IResult ToCreatedResponse<T>(this Result<T> result, string location)
    {
        return result.IsSuccess ? 
            TypedResults.Created(location, result.Value) : 
            BadRequest(result);
    }
    
    private static IResult BadRequest<T>(Result<T> result)
    {
        var error = result.Errors.First();
        var problemDetails = new ProblemDetails
        {
            Title = "Domain Error",
            Detail = error.Message,
            Status = StatusCodes.Status400BadRequest
        };

        return TypedResults.BadRequest(problemDetails);
    }
}
