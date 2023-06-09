using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using WebApplication1.Errors;

namespace WebApplication1.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFound ex)
            {
                await HandleExceptionAsync(HttpStatusCode.NotFound, ex.Message, context);
            }
            catch (BadRequest ex)
            {
                await HandleExceptionAsync(HttpStatusCode.BadRequest, ex.Message, context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(HttpStatusCode.InternalServerError, ex.Message, context);
            }
        }

        private async Task HandleExceptionAsync(HttpStatusCode httpStatusCode, string message, HttpContext httpContext)
        {
            var ObjErrorDetails = new ErrorDetails {
                StatusCode = httpStatusCode,
                Message = message
            };

            var errorData = JsonSerializer.Serialize(ObjErrorDetails);

            var response = httpContext.Response;
            await response.WriteAsync(errorData);
        }
    }


}
