using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SocialNet1.Models.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNet1.Hubs
{
    public class MessageHub : Hub
    {
        private readonly ILogger<MessageHub> _logger;
        public MessageHub(ILogger<MessageHub> logger)
        {
            _logger = logger;
        }

        public async Task Send(SimpMessageModel message)
        {
            message.Date = DateTime.Now.ToString("t");

            HttpClient client = new HttpClient();
            CancellationToken Cancel = default;

            //var response = client.PostAsJsonAsync<SimpMessageModel>(API.AddMessAPI, message, Cancel).Result.Content.ReadAsStringAsync().Result;

            //var responseresult = Convert.ToBoolean(response);

            var responseresult = true;

            if (responseresult)
                _logger.LogInformation($"Сообщение ({message.Content}) людей ({message.SenderName}) и " +
                    $"({message.RecipientName}) успешно сохранено в БД:)");
            else
                _logger.LogWarning($"Сообщение ({message.Content}) людей ({message.SenderName}) и " +
                    $"({message.RecipientName}) обломаломь с БД:(");

            await Clients.Users(message.SenderName, message.RecipientName).SendAsync("Receive", message);
        }
    }
}
