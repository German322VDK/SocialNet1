using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.Models;
using SocialNet1.Models.API;
using System.Linq;

namespace SocialNet1.Controllers.API
{
    [Route("api/image")]
    [ApiController]
    public class ImageAPIController : ControllerBase
    {
        private IUser _user;
        private readonly ILogger<ImageAPIController> _logger;

        public ImageAPIController(IUser user, ILogger<ImageAPIController> logger)
        {
            _user = user;
            _logger = logger;
        }

        
        [HttpPost("addcom")]
        public PhotoUserInfoComms Add(AddCommentImageModel model)
        {
            if(model is null)
            {
                _logger.LogWarning($"Не получилось добавить комментарий, данные не пришли(");

                return null;
            }

            string recipient = model.Recipient, text = model.Text, sender = model.Sender;

            int imageId = model.ImageId;

            var com = _user.AddCommentToPhoto(recipient, sender, text, imageId);

            _logger.LogInformation($"{sender} пытается добавить комментарий: {text} под фото {imageId} принадлежащее {recipient}");

            if (com == null)
            {
                _logger.LogWarning($"{sender} не смог добавить комментарий: {text} под фото {imageId} принадлежащее {recipient}");

                return null;
            }
                
            var senderUser = _user.Get(sender);

            var curImageArr = senderUser.Images.SingleOrDefault(el => el.ImageNumber == senderUser.SocNetItems.CurrentImage).Image;

            var format = ImageMethods.GetFormat(curImageArr);

            var curImage = ImageMethods.GetStringFromByteArr(curImageArr);

            _logger.LogInformation($"{sender} смог добавить комментарий: {text} под фото {imageId} принадлежащее {recipient}");

            return new PhotoUserInfoComms
            {
                DateTime = com.DateTime.ToString("g"),
                Comment = com.Text,
                //Likes = com.LikeCount,
                AuthorFirstName = senderUser.FirstName,
                AuthorSecondName = senderUser.SecondName,
                AuthorUserName = senderUser.UserName,
                AuthorCoordinatesImage = $"photo/coordinates/{senderUser.SocNetItems.X}d{senderUser.SocNetItems.Y}.jpg",
                AuthorImage = curImage,
                AuthorFormat = format,
                HelpId = com.HelpId
            };
        }

        [HttpGet("deletecom")]
        public bool DeleteCom(string imageAutor, int imageId, int comId)
        {
            if (string.IsNullOrEmpty(imageAutor))
            { 
                _logger.LogWarning($"Не получилось удалить комментарий потому что я не знаю что удалять(");

                return false;
            }

            _logger.LogInformation($"Пытаемся удалить комментарий {comId} под фото {imageId} человека {imageAutor}");

            var result = _user.DeleteComToPhoto(imageAutor, imageId, comId);

            if (!result)
            {
                _logger.LogWarning($"Не получилось удалить комментарий {comId} под фото {imageId} человека {imageAutor} (");

                return false;
            }

            _logger.LogInformation($"Получилось удалить комментарий {comId} под фото {imageId} человека {imageAutor} )");

            return true;
        }

        [HttpPost("delete")]
        public bool Delete(DeleteImageModel model)
        {
            if(model.UserName is null)
            {
                _logger.LogWarning("Удаление фото не получилось, пришли неправильные данные(");

                return false;
            }

            var result = _user.DeletePhoto(model.ImageId, model.UserName);

            if (!result)
            {
                _logger.LogWarning($"Удаление фото {model.ImageId} человека {model.UserName} не получилось(");

                return false;
            }

            _logger.LogInformation($"Удаление фото {model.ImageId} человека {model.UserName} получилось)");

            return true;
        }


        [HttpGet("addlike")]
        public bool AddLike(string username1, string username2, int imageid)
        {
            if (!(username1 is not null && username2 is not null))
            {
                _logger.LogWarning($"{username1 ?? ""} не смог поставить лайк {username2 ?? ""}!");
                return false;
            }

            var result = _user.AddLikePhoto(username1, username2, imageid);

            if (!result)
            {
                _logger.LogWarning($"{username1} уже до этого поставил лайк {username2} под фото {imageid}!");
            }
            else
            {
                _logger.LogInformation($"{username1} поставил лайк {username2} под фото {imageid}!");
            }

            return result;
        }

        [HttpGet("deletelike")]
        public bool DeleteLike(string username1, string username2, int imageid)
        {
            if (!(username1 is not null && username2 is not null))
            {
                _logger.LogWarning($"{username1 ?? ""} не смог убрать лайк {username2 ?? ""}!");
                return false;
            }

            var result = _user.DeleteLikePhoto(username1, username2, imageid);

            if (!result)
            {
                _logger.LogWarning($"{username1} ещё не ставил лайк {username2} под фото {imageid}!");
            }
            else
            {
                _logger.LogInformation($"{username1} убрал лайк {username2} под фото {imageid}!");
            }

            return result;
        }


        [HttpPost("addlikecom")]
        public bool AddLikeCom(LikeComModel model)
        {
            if (!(model.UserName1 is not null && model.UserName2 is not null))
            {
                _logger.LogWarning($"{model.UserName1 ?? ""} не смог поставить лайк {model.UserName2 ?? ""}!");
                return false;
            }

            var result = _user.AddLikeComPhoto(model.UserName1, model.UserName2, model.ImageId, model.ComId);

            if (!result)
            {
                _logger.LogWarning($"{model.UserName1} уже до этого поставил лайк {model.UserName2} под коммент {model.ComId} фото {model.ImageId}!");
            }
            else
            {
                _logger.LogInformation($"{model.UserName1} поставил лайк {model.UserName2} под коммент {model.ComId} фото {model.ImageId}!");
            }

            return result;
        }

        [HttpPost("deletelikecom")]
        public bool DeleteLikeCom(LikeComModel model)
        {
            if (!(model.UserName1 is not null && model.UserName2 is not null))
            {
                _logger.LogWarning($"{model.UserName1 ?? ""} не смог убрать лайк {model.UserName2 ?? ""}!");
                return false;
            }

            var result = _user.DeleteLikeComPhoto(model.UserName1, model.UserName2, model.ImageId, model.ComId);

            if (!result)
            {
                _logger.LogWarning($"{model.UserName1} ещё не ставил лайк {model.UserName2} под коммент {model.ComId} фото {model.ImageId}!");
            }
            else
            {
                _logger.LogInformation($"{model.UserName1} убрал лайк {model.UserName2} под коммент {model.ComId} фото {model.ImageId}!");
            }

            return result;
        }
    }
}
