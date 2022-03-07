using Microsoft.AspNetCore.SignalR;
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
        public MessageHub()
        {

        }

        //public async Task Send(SimpMessage message)
        //{
        //    message.Date = DateTime.Now.ToString("g");

        //    HttpClient client = new HttpClient();
        //    CancellationToken Cancel = default;

        //    var response = client.PostAsJsonAsync<SimpMessage>(API.AddMessAPI, message, Cancel).Result.Content.ReadAsStringAsync().Result;

        //    var responseresult = Convert.ToBoolean(response);

        //    if (responseresult)
        //        _Logger.LogInformation($"Сообщение ({message.Content}) людей ({message.UserName}) и " +
        //            $"({message.RecipientName}) успешно сохранено в БД:)");
        //    else
        //        _Logger.LogInformation($"Сообщение ({message.Content}) людей ({message.UserName}) и " +
        //            $"({message.RecipientName}) обломаломь с БД:(");

        //    await Clients.Users(message.UserName, message.RecipientName).SendAsync("Receive", message);
        //}
    }
}
