using System.Net;
using Tienda.Application.DTO;
using Tienda.Application.Exceptions;
using Tienda.Application.Mappers;
using Tienda.Domain.Exceptions;

namespace Tienda.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ErrorResponse errorResponse;
            string errorResponseJson = string.Empty;

            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case InvalidModelException e:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse = new ErrorResponse(context.Response.StatusCode, e.Errors);
                    errorResponseJson = errorResponse.ToString();
                    //await logger.WriteErrorAsync(LogProcessType.DocumentManagerApi, errorResponseJson);
                    break;
                case BadRequestDomainException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse = new ErrorResponse(context.Response.StatusCode, ErrorParser.ParseSingleError(exception.Message));
                    errorResponseJson = errorResponse.ToString();
                    //await logger.WriteErrorAsync(LogProcessType.DocumentManagerApi, exception);
                    break;
                //case ForbiddenException:
                //    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                //    errorResponse = new ErrorResponse(context.Response.StatusCode, ErrorParser.ParseSingleError(exception.Message));
                //    errorResponseJson = errorResponse.ToString();
                //    await logger.WriteWarningAsync(LogProcessType.DocumentManagerApi, exception);
                //    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    //var logId = await logger.WriteUnhandledErrorAsync(exception);
                    errorResponse = new ErrorResponse(context.Response.StatusCode, ErrorParser.ParseSingleError($"Internal Server Error."));
                    errorResponseJson = errorResponse.ToString();
                    break;
            }

            await context.Response.WriteAsync(errorResponseJson);
        }
    }
}
