using Application.Exceptions;
using Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Facturador.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Sigue el camino normal
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Si es un error de negocio (como el de la factura pagada), devolvemos 400
            // Si es cualquier otra cosa (explotó la DB), devolvemos 500
            var statusCode = exception switch
            {
                BusinessException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var response = new
            {
                status = statusCode,
                message = exception.Message,
                // Solo para desarrollo: podrías agregar el stacktrace si no es BusinessException
                detail = statusCode == 500 ? "Error interno del servidor" : null
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}