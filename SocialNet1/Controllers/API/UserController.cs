using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Domain.PostCom;
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

        [HttpGet("deletepost")]
        public bool DeletePost(string userName, int postId)
        {
            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogWarning("Не понятно кому удалять пост");

                return false;
            }

            var result = _user.DeletePost(userName, postId);

            if (!result)
            {
                _logger.LogInformation($"На странице человека {userName} не получилось удалить пост № {postId}");

                return false;
            }

            _logger.LogInformation($"На странице человека {userName} был удалён пост № {postId}");

            return true;
        }

        [HttpGet("addlikepost")]
        public bool AddLikePost(string userName, int postId, string liker)
        {
            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(liker))
            {
                _logger.LogWarning("Владелец поста или лайкер не пришёл(");

                return false;
            }

            var result = _user.AddPostLike(userName, postId, liker);

            if (!result)
            {
                _logger.LogWarning($"Человек {liker} почему-то не смог лайкнуть пост № {postId} человека {userName}(");

                return false;
            }

            _logger.LogInformation($"Человек {liker} смог лайкнуть пост № {postId} человека {userName})");

            return true;
        }

        [HttpGet("deletelikepost")]
        public bool DeleteLikePost(string userName, int postId, string liker)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(liker))
            {
                _logger.LogWarning("Владелец поста или лайкер не пришёл(");

                return false;
            }

            var result = _user.DeletePostLike(userName, postId, liker);

            if (!result)
            {
                _logger.LogWarning($"Человек {liker} почему-то не смог убрать лайк с поста № {postId} человека {userName}(");

                return false;
            }

            _logger.LogInformation($"Человек {liker} смог убрать лайк с поста № {postId} человека {userName})");

            return true;
        }

        [HttpPost("addcompost")]

        public CommentDTO AddComPost(AddCommentPostModel model)
        {
            if(model is null || string.IsNullOrEmpty(model.Commenter) || string.IsNullOrEmpty(model.Text) || string.IsNullOrEmpty(model.UserName))
            {
                _logger.LogWarning("Данные не пришли или пришли повреждёнными(");

                return null;
            }

            var result = _user.AddPostCom(model.UserName, model.PostId, model.Commenter, model.Text);

            if(result is null)
            {
                _logger.LogWarning($"Не получилось добавить коммент '{model.Text}' человека {model.Commenter} под постом № {model.PostId} " +
                    $"человека {model.UserName} (");

                return null;
            }

            _logger.LogInformation($"Получилось добавить коммент '{model.Text}' человека {model.Commenter} под постом № {model.PostId} " +
                    $"человека {model.UserName} )");

            return result;

        }
    }
}
