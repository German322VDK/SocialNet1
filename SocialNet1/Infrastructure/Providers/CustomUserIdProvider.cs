using Microsoft.AspNetCore.SignalR;

namespace Social_Net1.Infrastructure.Providers
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection) =>
            connection.User?.Identity.Name;
    }
}
