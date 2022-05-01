using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SocialNet1.Models.Hub;
using SocialNet1.Models.Static;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNet1.Hubs
{
    public class SecretMessageHub : Hub
    {
        private string url = "http://localhost:5000/";

        private readonly ILogger<SecretMessageHub> _logger;
        public SecretMessageHub(ILogger<SecretMessageHub> logger)
        {
            _logger = logger;
        }

        public async Task Send(SimpMessageModel message)
        {
            if (message is null || string.IsNullOrEmpty(message.Content) || string.IsNullOrEmpty(message.SenderName)
                || string.IsNullOrEmpty(message.RecipientName))
            {
                _logger.LogWarning("Сообщение не пришло (");

                return;
            }

            message.Date = DateTime.Now.ToString("t");

            await Clients.Users(message.SenderName, message.RecipientName).SendAsync("Receive", message);
        }

    }
}
