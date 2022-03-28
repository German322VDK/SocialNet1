using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Controllers.API
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _user;
        private readonly ILogger<UserController> _logger;

        public UserController(IUser user, ILogger<UserController> logger)
        {
            _user = user;
            _logger = logger;
        }

        //[HttpGet("setstatus")]
        //public string SetStatus(string text, string username)
        //{
        //    _logger.LogInformation($"Тип {username ?? ""} пытается поменять статус на '{text}'");

        //    var result = _user.SetStatus(text, username);

        //    if (!result)
        //    {
        //        _logger.LogInformation($"Тип {username ?? ""} не смог поменять статус на '{text}'");

        //        return "Не получилось поменять статус:(";
        //    }


        //    _logger.LogInformation($"Тип {username} смог поменять статус на '{text}'");

        //    return text;
        //}

        [HttpPost("setstatus")]
        public string SetStatus(SetStatusModel model)
        {
            if (model is null)
            {
                return "Ничего не пришло";
            }

            _logger.LogInformation($"Тип {model.UserName ?? ""} пытается поменять статус на '{model.Text}'");

            var result = _user.SetStatus(model.Text, model.UserName);

            if (!result)
            {
                _logger.LogInformation($"Тип {model.UserName ?? ""} не смог поменять статус на '{model.Text}'");

                return "Не получилось поменять статус:(";
            }


            _logger.LogInformation($"Тип {model.UserName} смог поменять статус на '{model.Text}'");

            return model.Text;
        }
    }
}
