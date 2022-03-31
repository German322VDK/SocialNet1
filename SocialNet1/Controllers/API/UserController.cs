using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Models.API;

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

        [HttpPost("setstatus")]
        public string SetStatus(SetStatusModel model)
        {
            if (model is null)
            {
                _logger.LogWarning($"Ничего не пришло(");

                return "Ничего не пришло";
            }

            _logger.LogInformation($"Тип {model.UserName ?? ""} пытается поменять статус на '{model.Text}'");

            var result = _user.SetStatus(model.Text, model.UserName);

            if (!result)
            {
                _logger.LogWarning($"Тип {model.UserName ?? ""} не смог поменять статус на '{model.Text}'");

                return "Не получилось поменять статус:(";
            }


            _logger.LogInformation($"Тип {model.UserName} смог поменять статус на '{model.Text}'");

            return model.Text;
        }
    }
}
