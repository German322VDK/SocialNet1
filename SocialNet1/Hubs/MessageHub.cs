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
    public class MessageHub : Hub
    {
        private string url = "http://localhost:5000/";

        private readonly ILogger<MessageHub> _logger;
        public MessageHub(ILogger<MessageHub> logger)
        {
            _logger = logger;
        }

        public async Task Send(SimpMessageModel message)
        {
            if(message is null || string.IsNullOrEmpty(message.Content) || string.IsNullOrEmpty(message.SenderName)
                || string.IsNullOrEmpty(message.RecipientName))
            {
                _logger.LogWarning("Сообщение не пришло (");

                return;
            }

            message.Date = DateTime.Now.ToString("t");

            var client = new HttpClient();
            CancellationToken Cancel = default;

            var response = client.PostAsJsonAsync($"{url}{APIUrls.ADD_MESSAGE}", message, Cancel).Result.Content.ReadAsStringAsync().Result;

            var responseresult = Convert.ToInt32(response);

            //var responseresult = true;

            if (responseresult != 0)
                _logger.LogInformation($"Сообщение '{message.Content}' людей '{message.SenderName}' и " +
                    $"({message.RecipientName}) успешно сохранено в БД:)");
            else
                _logger.LogWarning($"Сообщение '{message.Content}' людей '{message.SenderName}' и " +
                    $"({message.RecipientName}) обломаломь с БД:(");

            message.MessageHelpId = responseresult;

            await Clients.Users(message.SenderName, message.RecipientName).SendAsync("Receive", message);
        }

        public async Task ToDelete(SimpMessageDeleteModel message)
        {
            if (message is null || string.IsNullOrEmpty(message.SenderName) || string.IsNullOrEmpty(message.RecipientName))
            {
                _logger.LogWarning("Не знаю какое сообщение удалять (");

                return;
            }

            var client = new HttpClient();
            CancellationToken Cancel = default;

            var response = client.PostAsJsonAsync($"{url}{APIUrls.DELETE_MESSAGE}", message, Cancel).Result.Content.ReadAsStringAsync().Result;

            bool responseresult = Convert.ToBoolean(response);

            if (responseresult)
                _logger.LogInformation($"Сообщение № {message.MessageHelpId} людей '{message.SenderName}' и " +
                    $"({message.RecipientName}) успешно удалено из БД:)");
            else
                _logger.LogWarning($"Сообщение № {message.MessageHelpId} людей '{message.SenderName}' и " +
                    $"({message.RecipientName}) обломаломь с удалением в БД:(");

            message.IsSuccess = responseresult;

            await Clients.Users(message.SenderName, message.RecipientName).SendAsync("FromDelete", message);
        }
    }
}
