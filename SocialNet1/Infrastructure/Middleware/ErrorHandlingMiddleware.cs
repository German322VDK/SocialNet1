using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Social_Net1.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate Next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = Next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext Context)
        {
            try
            {
                await _next(Context);
            }
            catch (Exception error)
            {
                HandleException(error, Context);
                //throw;

                //await Context.Response.WriteAsync("Token is invalid");

                Context.Response.Redirect("/Home/Error");
            }
        }

        private void HandleException(Exception error, HttpContext context)
        {
            _logger.LogError(error, "Ошибка при обработке запроса к {0}",
                context.Request.Path);
        }
    }
}
