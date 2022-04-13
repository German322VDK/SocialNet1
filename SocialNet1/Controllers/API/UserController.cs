using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Domain.PostCom;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.Models.API;
using System.Linq;

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
        public CommentModel AddComPost(AddCommentPostModel model)
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

            var commenter = _user.Get(result.CommentatorName);

            var color = UserMethods.GetColor(commenter.SocNetItems.X, commenter.SocNetItems.Y);

            var imageArr = commenter.Images.SingleOrDefault(el => el.ImageNumber == commenter.SocNetItems.CurrentImage).Image;

            var strImage = NewImageMethods.GetStringFromByteArr(imageArr);

            var format = NewImageMethods.GetFormat(imageArr);

            return new CommentModel
            {
                X = commenter.SocNetItems.X,
                Y = commenter.SocNetItems.Y,
                FirstName = commenter.FirstName,
                SecondName = commenter.SecondName,
                LikesCount = 0,
                Color = color,
                DateTime = result.DateTime.ToString("g"),
                PhotoCom = strImage,
                FormatPhotoCom = format,
                Text = result.Content
            };

        }

        [HttpGet("deletecompost")]
        public bool DeleteComPost(string user, int postId, int comId)
        {
            if (string.IsNullOrEmpty(user))
            {
                _logger.LogWarning("Не понятно кому удалять коммент поста (");

                return false;
            }

            var result = _user.DeletePostCom(user, postId, comId);

            if (!result)
            {
                _logger.LogWarning($"Не получилось удалить коммент № {comId} поста № {postId} человека {user} (");

                return false;
            }

            _logger.LogInformation($"Получилось удалить коммент № {comId} поста № {postId} человека {user} )");

            return true;
        }

        [HttpPost("addlikecompost")]
        public bool AddLikeComPost(LikeComPostModel model)
        {
            if(model is null || string.IsNullOrEmpty(model.UserName1) || string.IsNullOrEmpty(model.UserName2))
            {
                _logger.LogWarning("Не получается поставить лайк на коммент поста, данные повреждены (");

                return false;
            }

            var result = _user.AddPostComLike(model.UserName1, model.PostId, model.ComId, model.UserName2);

            if (!result)
            {
                _logger.LogWarning($"Почему-то не получается человеку {model.UserName2} поставить лайк на коммент № {model.ComId} поста № " +
                    $"{model.PostId} со страници человека {model.UserName1} (");

                return false;
            }

            _logger.LogInformation($"Человек {model.UserName2} смог поставить лайк на коммент № {model.ComId} поста № " +
                    $"{model.PostId} со страници человека {model.UserName1} )");

            return true;
        }

        [HttpPost("deletelikecompost")]
        public bool DeleteLikeComPost(LikeComPostModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.UserName1) || string.IsNullOrEmpty(model.UserName2))
            {
                _logger.LogWarning("Не получается убрать лайк с коммента поста, данные повреждены (");

                return false;
            }

            var result = _user.DeletePostComLike(model.UserName1, model.PostId, model.ComId, model.UserName2);

            if (!result)
            {
                _logger.LogWarning($"Почему-то не получается человеку {model.UserName2} убрать лайк на коммент № {model.ComId} поста № " +
                    $"{model.PostId} со страници человека {model.UserName1} (");

                return false;
            }

            _logger.LogInformation($"Человек {model.UserName2} смог убрать лайк на коммент № {model.ComId} поста № " +
                    $"{model.PostId} со страници человека {model.UserName1} )");

            return true;
        }
    }
}
