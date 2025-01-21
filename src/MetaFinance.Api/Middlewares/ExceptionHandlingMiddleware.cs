using System.Net;
using System.Text.Json;
using MetaFinance.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MetaFinance.Api.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger,
    IWebHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case ApplicationException:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails.Detail = exception.Message;
                problemDetails.Title = "Application Error";
                break;
            case ValidationException ex:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails = new ValidationProblemDetails(ex.Errors);
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                problemDetails.Detail = ex.Message;
                problemDetails.Extensions.Add("invalidParams", ex.Errors);
                problemDetails.Title = "Validation Error";
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails.Detail = env.IsDevelopment() ? $"{exception.Message} - {exception.StackTrace}" : "Internal Server error. Check Logs!";
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
                problemDetails.Title = "Server error";
                break;
        }
        
        logger.LogError(exception, "Exception in request");
        var result = JsonSerializer.Serialize(problemDetails);
        await context.Response.WriteAsync(result);
    }
}