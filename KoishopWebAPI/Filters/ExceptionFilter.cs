using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System;
using KoishopServices.Common.Exceptions;

namespace KoishopWebAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {               
                case ForbiddenAccessException:
                    context.Result = new ForbidResult();
                    context.ExceptionHandled = true;
                    break;
                case UnauthorizedAccessException:
                    context.Result = new ForbidResult();
                    context.ExceptionHandled = true;
                    break;
                case NotFoundException exception:
                    context.Result = new NotFoundObjectResult(new ProblemDetails
                    {
                        Detail = exception.Message
                    })
                    .AddContextInformation(context);
                    context.ExceptionHandled = true;
                    break;
                case DuplicationException exception:
                    context.Result = new BadRequestObjectResult(new ProblemDetails
                    {
                        Detail = exception.Message
                    })
                        .AddContextInformation(context);
                    context.ExceptionHandled = true;
                    break;
                case UnauthorizedException exception:
                    context.Result = new UnauthorizedObjectResult(new ProblemDetails
                    {
                        Detail = exception.Message
                    })
                        .AddContextInformation(context);
                    context.ExceptionHandled = true;
                    break;


            }
        }
    }

    internal static class ProblemDetailsExtensions
    {
        public static IActionResult AddContextInformation(this ObjectResult objectResult, ExceptionContext context)
        {
            if (objectResult.Value is not ProblemDetails problemDetails)
            {
                return objectResult;
            }
            problemDetails.Extensions.Add("traceId", Activity.Current?.Id ?? context.HttpContext.TraceIdentifier);

            return objectResult;
        }
    }
}
