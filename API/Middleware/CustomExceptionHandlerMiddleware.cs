using Application.Common.Exceptions;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            if (exception as FluentValidation.ValidationException != null)
            {
                code = HttpStatusCode.BadRequest;
                foreach (FluentValidation.Results.ValidationFailure item in ((FluentValidation.ValidationException)exception).Errors)
                {
                    result += item.ErrorMessage;
                }

                result = JsonSerializer.Serialize(new { code = (int)code, error = result });
            }
            else
            {
                switch (exception)
                {
                    case CustomHttpException customHttpException:
                        code = customHttpException.StatusCode;
                        result = JsonSerializer.Serialize(new { code = (int)code, error = customHttpException.Message });
                        break;
                    case NotFoundException:
                        code = HttpStatusCode.NotFound;
                        break;
                }
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message, trace = exception.StackTrace});
            }

            return context.Response.WriteAsync(result);
        }
    }
}
