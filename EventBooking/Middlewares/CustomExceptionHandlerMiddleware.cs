using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Domain.BaseException;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace EventBooking.API.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var check = context.User.Identity.IsAuthenticated;
                await _next(context);
            }
            catch (CoreException ex)
            {
                await HandleCoreExceptionAsync(context, ex);
            }
            catch (ErrorException ex)
            {
                await HandleErrorExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGeneralExceptionAsync(context, ex);
            }
        }

        private Task HandleCoreExceptionAsync(HttpContext context, CoreException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;
            var result = JsonSerializer.Serialize(new { ex.Code, ex.Message, ex.AdditionalData });
            return context.Response.WriteAsync(result);
        }

        private Task HandleErrorExceptionAsync(HttpContext context, ErrorException ex)
        {
            _logger.LogError(ex, ex.ErrorDetail.ErrorMessage.ToString());
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;
            var result = JsonSerializer.Serialize(ex.ErrorDetail);
            return context.Response.WriteAsync(result);
        }

        private Task HandleGeneralExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new { error = "An unexpected error occurred. Please try again later." });
            return context.Response.WriteAsync(result);
        }
    }
}

