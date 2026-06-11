using DotCruz.CoreAuth.Application.DTOs.Base;
using DotCruz.CoreAuth.Domain.Exceptions.BaseExceptions;
using DotCruz.CoreAuth.Domain.Exceptions.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotCruz.CoreAuth.Api.Filters;

public class ExceptionFilter(ILogger<ExceptionFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CoreAuthException exception)
        {
            HandleProjectException(exception, context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleProjectException(CoreAuthException exception, ExceptionContext context)
    {
        var statusCode = (int)exception.GetStatusCode();
        var errors = exception.GetErrorsMessages();

        logger.LogWarning(
            context.Exception,
            "Handled business exception. Status: {StatusCode}. Errors: {@Errors}",
            statusCode,
            errors);

        context.HttpContext.Response.StatusCode = statusCode;
        context.Result = new ObjectResult(new ErrorResponseDto(errors));

        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        logger.LogError(
            context.Exception,
            "Unhandled exception caught in Controller: {ControllerName}, Method: {HttpMethod}, Error: {Message}",
            context.ActionDescriptor.DisplayName,
            context.HttpContext.Request.Method,
            context.Exception.Message);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ErrorResponseDto(ResourceMessagesException.UNKNOWN_ERROR));

        context.ExceptionHandled = true;
    }
}
