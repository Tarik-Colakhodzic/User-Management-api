using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly IDictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

        public GlobalExceptionHandlingMiddleware()
        {
            _exceptionHandlers = new Dictionary<Type, Func<HttpContext, Exception, Task>>
            {
                { typeof(ApplicationException), HandleApplicationExceptionAsync }
            };
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Type type = exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                await _exceptionHandlers[type].Invoke(context, exception);
                return;
            }

            await HandleUnknownExceptionAsync(context);
        }

        private async Task HandleUnknownExceptionAsync(HttpContext context)
        {
            ProblemDetails details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Detail = "An error occurred while processing your request."
            };

            await WriteErrorToResponse(context, details);
        }

        private async Task HandleApplicationExceptionAsync(HttpContext context, Exception exception)
        {
            ApplicationException applicationException = exception as ApplicationException;

            ProblemDetails details = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = applicationException.GetType().ToString(),
                Title = applicationException.Message,
                Detail = applicationException.InnerException?.Message
            };

            await WriteErrorToResponse(context, details);
        }

        private async Task WriteErrorToResponse(HttpContext context, ProblemDetails problemDetails)
        {
            context.Response.StatusCode = (int)problemDetails.Status;
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
            context.Response.ContentType = "application/json";
        }
    }
}