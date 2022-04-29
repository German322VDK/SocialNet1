using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.Models.API;
using System.Linq;

namespace SocialNet1.Controllers.API
{
    [Route("api/group")]
    [ApiController]
    public class GroupAPIController : ControllerBase
    {
        private readonly ILogger<GroupAPIController> _logger;
        private readonly IGroup _group;
        private readonly IUser _user;

        public GroupAPIController(ILogger<GroupAPIController> logger, IGroup group, IUser user)
        {
            _logger = logger;
            _group = group;
            _user = user;
        }

        [HttpGet("deletepost")]
        public bool DeletePost(string groupName, int postId)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                _logger.LogWarning("Не понятно кому удалять пост");

                return false;
            }

            if (_group.Get(groupName) is null)
            {
                _logger.LogWarning($"Не существует группы {groupName}");

                return false;
            }

            var result = _group.DeletePost(groupName, postId);

            if (!result)
            {
                _logger.LogInformation($"На странице группы {groupName} не получилось удалить пост № {postId}");
            }
            else
            {
                _logger.LogInformation($"На странице группы {groupName} был удалён пост № {postId}");
            }

            return result;
        }

        //[HttpPost("delete")]
        //public bool DeleteImage(DeleteGroupImageModel model)
        //{
        //    if (string.IsNullOrEmpty(model.GroupName))
        //    {
        //        _logger.LogWarning($"Не понятно какой группе удалять фото");

        //        return false;
        //    }

        //    if (_group.Get(model.GroupName) is null)
        //    {
        //        _logger.LogWarning($"Не существует группы {model.GroupName}");

        //        return false;
        //    }

        //    var result = _group.DeletePhoto(model.GroupName, model.ImageId);

        //    if (result)
        //    {
        //        _logger.LogInformation($"Получилось удалить фото {model.ImageId} группы {model.GroupName}");
        //    }
        //    else
        //    {
        //        _logger.LogWarning($"Не получилось удалить фото {model.ImageId} группы {model.GroupName}");
        //    }

        //    return result;
        //}

        [HttpGet("addlikepost")]
        public bool AddPostLike(string groupName, string liker, int postId)
        {
            if (string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(liker))
            {
                _logger.LogWarning($"Не понятно кто кому ставит лайк");

                return false;
            }

            if (_user.Get(liker) is null || _group.Get(groupName) is null)
            {
                _logger.LogWarning($"Не существует группы {groupName} или человека {liker}");

                return false;
            }

            var result = _group.AddPostLike(groupName, liker, postId);

            if (result)
            {
                _logger.LogInformation($"Получилось добавить лайк под пост {postId} группы {groupName} человеку {liker}");
            }
            else
            {
                _logger.LogWarning($"Не получилось добавить лайк под пост {postId} группы {groupName} человеку {liker}");
            }

            return result;
        }

        [HttpGet("deletelikepost")]
        public bool DeletePostLike(string groupName, string liker, int postId)
        {
            if (string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(liker))
            {
                _logger.LogWarning($"Не понятно кто кому ставит лайк");

                return false;
            }

            if (_user.Get(liker) is null || _group.Get(groupName) is null)
            {
                _logger.LogWarning($"Не существует группы {groupName} или человека {liker}");

                return false;
            }

            var result = _group.DeletePostLike(groupName, liker, postId);

            if (result)
            {
                _logger.LogInformation($"Получилось убрать лайк с поста {postId} группы {groupName} человеку {liker}");
            }
            else
            {
                _logger.LogWarning($"Не получилось убрать лайк с поста {postId} группы {groupName} человеку {liker}");
            }

            return result;
        }

        [HttpPost("addcompost")]
        public CommentModel AddComPost(AddCommentPostModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.Commenter) || string.IsNullOrEmpty(model.Text) || string.IsNullOrEmpty(model.UserName))
            {
                _logger.LogWarning("Данные не пришли или пришли повреждёнными(");

                return null;
            }

            var result = _group.AddPostCom(model.UserName, model.PostId, model.Commenter, model.Text);

            if (result is null)
            {
                _logger.LogWarning($"Не получилось добавить коммент '{model.Text}' человека {model.Commenter} под постом № {model.PostId} " +
                    $"группы {model.UserName} (");

                return null;
            }

            _logger.LogInformation($"Получилось добавить коммент '{model.Text}' человека {model.Commenter} под постом № {model.PostId} " +
                    $"группы {model.UserName} )");

            var commenter = _user.Get(result.CommentatorName);

            var color = UserMethods.GetColor(commenter.SocNetItems.X, commenter.SocNetItems.Y);

            var imageArr = commenter.Images.SingleOrDefault(el => el.ImageNumber == commenter.SocNetItems.CurrentImage).Image;

            var strImage = NewImageMethods.GetStringFromByteArr(imageArr);

            var format = NewImageMethods.GetFormat(imageArr);

            return new CommentModel
            {
                Id = model.PostId,
                X = commenter.SocNetItems.X,
                Y = commenter.SocNetItems.Y,
                FirstName = commenter.FirstName,
                SecondName = commenter.SecondName,
                LikesCount = 0,
                Color = color,
                DateTime = result.DateTime.ToString("g"),
                PhotoCom = strImage,
                FormatPhotoCom = format,
                Text = result.Content,
                HelpId = result.HelpId,
                Author = model.UserName,
                Commenter = model.Commenter
            };
        }

        [HttpGet("deletecompost")]
        public bool DeletePostCom(string groupName, int postId, int comId)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                _logger.LogWarning("Не понятно кому удалять коммент поста");

                return false;
            }

            if (_group.Get(groupName) is null)
            {
                _logger.LogWarning($"Не существует группы {groupName}");

                return false;
            }

            var result = _group.DeleteComPost(groupName, postId, comId);

            if (!result)
            {
                _logger.LogInformation($"На странице группы {groupName} не получилось удалить коммент № {comId} поста № {postId}");
            }
            else
            {
                _logger.LogInformation($"На странице группы {groupName} был удалён коммент № {comId} поста № {postId}");
            }

            return result;
        }

        [HttpPost("addlikecompost")]
        public bool AddLikeComPost(LikeComGroupPostModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.GroupName) || string.IsNullOrEmpty(model.UserName))
            {
                _logger.LogWarning("Не получается поставить лайк на коммент поста, данные повреждены (");

                return false;
            }

            if(_user.Get(model.UserName) is null || _group.Get(model.GroupName) is null)
            {
                _logger.LogWarning($"Человека {model.UserName} или группы {model.GroupName} не существует(");

                return false;
            }

            var result = _group.AddPostComLike(model.GroupName, model.PostId, model.ComId, model.UserName);

            if (!result)
            {
                _logger.LogWarning($"Почему-то не получается человеку {model.UserName} поставить лайк на коммент № {model.ComId} поста № " +
                    $"{model.PostId} со страници группы {model.GroupName} (");

                return false;
            }

            _logger.LogInformation($"Человек {model.UserName} смог поставить лайк на коммент № {model.ComId} поста № " +
                    $"{model.PostId} со страници группы {model.GroupName} )");

            return true;
        }

        [HttpPost("deletelikecompost")]
        public bool DeleteLikeComPost(LikeComGroupPostModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.GroupName) || string.IsNullOrEmpty(model.UserName))
            {
                _logger.LogWarning("Не получается убрать лайк на коммент поста, данные повреждены (");

                return false;
            }

            if (_user.Get(model.UserName) is null || _group.Get(model.GroupName) is null)
            {
                _logger.LogWarning($"Человека {model.UserName} или группы {model.GroupName} не существует(");

                return false;
            }

            var result = _group.DeletePostComLike(model.GroupName, model.PostId, model.ComId, model.UserName);

            if (!result)
            {
                _logger.LogWarning($"Почему-то не получается человеку {model.UserName} убрать лайк на коммент № {model.ComId} поста № " +
                    $"{model.PostId} со страници группы {model.GroupName} (");

                return false;
            }

            _logger.LogInformation($"Человек {model.UserName} смог убрать лайк на коммент № {model.ComId} поста № " +
                    $"{model.PostId} со страници группы {model.GroupName} )");

            return true;
        }

        [HttpGet("sub")]
        public bool Sub(string groupName, string userName)
        {
            if(string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(userName))
            {
                _logger.LogWarning("Не понятно кто на кого подписывается(");

                return false;
            }

            if (_group.Get(groupName) is null || _user.Get(userName) is null)
            {
                _logger.LogWarning($"Группы {groupName} или пользователя {userName} не существует(");

                return false;
            }

            var result = _group.Sub(groupName, userName);

            if (result)
            {
                _logger.LogInformation($"Получилось подписать человека {userName} в группу {groupName}");
            }
            else
            {
                _logger.LogWarning($"Не получилось подписать человека {userName} в группу {groupName}(");
            }

            return result;
        }

        [HttpGet("unsub")]
        public bool UnSub(string groupName, string userName)
        {
            if (string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(userName))
            {
                _logger.LogWarning("Не понятно кто на кого подписывается(");

                return false;
            }

            if (_group.Get(groupName) is null || _user.Get(userName) is null)
            {
                _logger.LogWarning($"Группы {groupName} или пользователя {userName} не существует(");

                return false;
            }

            var result = _group.UnSub(groupName, userName);

            if (result)
            {
                _logger.LogInformation($"Получилось отписать человека {userName} в группу {groupName}");
            }
            else
            {
                _logger.LogWarning($"Не получилось отписать человека {userName} в группу {groupName}(");
            }

            return result;
        }
    }
}
