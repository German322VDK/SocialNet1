using Microsoft.AspNetCore.Builder;

namespace Social_Net1.Infrastructure.Middleware
{
    public static class OnlineUsersMiddlewareExtensions
    {
        public static void UseOnlineUsers(this IApplicationBuilder app, string cookieName = "UserGuid", int lastActivityMinutes = 20)
        {
            app.UseMiddleware<OnlineUsersMiddleware>(cookieName, lastActivityMinutes);
        }
    }
}
