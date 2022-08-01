using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Data;
using SocialNet1.Domain.Identity;
using SocialNet1.Domain.PostCom;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.Models;
using SocialNet1.ViewModels;
using System.Collections.Generic;

namespace SocialNet1.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IUser _user;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IUser user, ILogger<ProfileController> logger)
        {
            _user = user;
            _logger = logger;
        }

        public IActionResult Index(string userName = null)
        {
            if (_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            UserDTO userDTO;

            var currantUserName = User.Identity!.Name;

            if (userName is not null)
                userDTO = _user.Get(userName);
            else
                userDTO = _user.Get(currantUserName);

            if(userDTO is null)
            {
                _logger.LogWarning($"Cтраницы {userName} нет(");

                return RedirectToAction("Index", "News");
            }

            _logger.LogInformation($"{currantUserName} заходит на страницу {userDTO.UserName}");

            var x = userDTO.SocNetItems.X;
            var y = userDTO.SocNetItems.Y;

            var coordName = CooridnatesNames.GetName(x, y);

            return View(new ProfileViewModel 
            { 
                User = userDTO,
                CoordName = coordName,
                CoordImage = $"photo/coordinates/{x}d{y}.jpg"
            });
        }

        public IActionResult AddImage(IFormFile uploadedFile)
        {
            if (_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            var currantUserName = User.Identity!.Name;

            _logger.LogInformation($"{currantUserName} пытается добавить фотографию");

            if (uploadedFile is null)
            {
                _logger.LogWarning($"Фото не пришло(");

                return RedirectToAction("Index", "News");
            }
                

            var arr = NewImageMethods.GetByteArrFromFile(uploadedFile);

            var result = NewImageMethods.IsValid(arr);

            if (!result)
            {
                _logger.LogWarning($"Фото плохое(");

                return RedirectToAction("Index", "News");
            }
               
            var resultAdding = _user.AddPhoto(arr, HttpContext.User.Identity.Name);

            if (!resultAdding)
            {
                _logger.LogWarning($"Фото почему то не добавилось(");

                return RedirectToAction("Index", "News");
            }

            _logger.LogInformation($"{currantUserName} успешно добавил фотографию");

            return RedirectToAction("Index", "Profile");
        }

        public IActionResult SetAva(string userName, int ava)
        {
            if (_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            if (userName is null)
            {
                _logger.LogWarning($"Не понятно кому менять аву(");

                return RedirectToAction("Index", "Profile");
            }

            var result = _user.SetAva(ava, userName);

            if (!result)
            {
                _logger.LogWarning($"Человек {userName} не смог выбрать авой фото № {ava}");

                return RedirectToAction("Index", "Profile");
            }

            _logger.LogInformation($"Человек {userName} смог выбрать авой фото № {ava}");

            return RedirectToAction("Index", "Profile");
        }

        public IActionResult SetCoord(string userName, int x, int y)
        {
            if (_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            if (userName is null)
            {
                _logger.LogWarning($"Не понятно кому менять взгляды(");

                return RedirectToAction("Index", "Profile");
            }

            var result = _user.SetCoord(userName, x, y);

            if (!result)
            {
                _logger.LogWarning($"Человек {userName} не смог выбрать взгляды x:{x};y:{y}");

                return RedirectToAction("Index", "Profile");
            }

            _logger.LogInformation($"Человек {userName} смог выбрать взгляды x:{x};y:{y}");

            return RedirectToAction("Index", "Profile");
        }

        public IActionResult AddComm(ComModel model)
        {
            if (_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            if (model is null || model.UserName is null || model.AuthorName is null || (model.uploadedFile is null && string.IsNullOrEmpty(model.Text)))
            {
                _logger.LogWarning("Данные для добавления поста не пришли");

                return RedirectToAction("Index", "Profile");
            }

            PostDTO post;
            string text = string.IsNullOrEmpty(model.Text) ? "" : model.Text;

            if (model.uploadedFile is not null)
            {

                var arr = NewImageMethods.GetByteArrFromFile(model.uploadedFile);

                var resultArr = NewImageMethods.IsValid(arr);

                if (resultArr)
                {
                    _logger.LogInformation($"Получилось добавить пост с картинкой: {text} человека {model.UserName} для человека {model.AuthorName}");

                    post = new PostDTO
                    {
                        ThisPost = new CommentDTO
                        {
                            CommentatorStatus = CommentatorStatus.User,
                            CommentatorName = model.UserName,
                            Content = text,
                            Images = new List<CommentImages>() 
                            { 
                                new CommentImages 
                                { 
                                    ImageNumber = 1,
                                    Image = arr
                                } 
                            }
                        }
                    };
                }
                else
                {
                    _logger.LogWarning($"Не получилось добавить пост с картинкой: {text} человека {model.UserName} для человека {model.AuthorName}");
                    post = new PostDTO
                    {
                        ThisPost = new CommentDTO
                        {
                            CommentatorStatus = CommentatorStatus.User,
                            CommentatorName = model.UserName,
                            Content = text
                        }
                    };
                }
            }
            else
            {
                _logger.LogInformation($"Получилось добавить пост без картинкой: {text} человека {model.UserName} для человека {model.AuthorName}");

                post = new PostDTO
                {
                    ThisPost = new CommentDTO
                    {
                        CommentatorStatus = CommentatorStatus.User,
                        CommentatorName = model.UserName,
                        Content = text
                    }
                };
            }

            var result = _user.AddUserPost(model.AuthorName, post);

            if (!result)
            {
                _logger.LogWarning($"Не получилось добавить пост: {model.Text} человека {model.UserName} для человека {model.AuthorName}");
                return RedirectToAction("Index", "Profile");
            }

            _logger.LogInformation($"Получилось добавить пост: {model.Text} человека {model.UserName} для человека {model.AuthorName}");

            return RedirectToAction("Index", "Profile", new { userName = model.AuthorName });
        }

        public IActionResult Delete(string userName)
        {
            var user = _user.Get(User.Identity.Name);

            if (user is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            var godResult = _user.IsUserInRole(user.UserName, "God");

            if (!godResult)
            {
                _logger.LogWarning($"Хакер {user.UserName} нас обманывает (");

                return RedirectToAction("Error", "Home");
            }

            var result = _user.Delete(userName);

            if (!result)
            {
                _logger.LogWarning($"Не получилось удалить типа {userName} (");
            }
            else
            {
                _logger.LogInformation($"Получилось удалить типа {userName}");
            }

            return RedirectToAction("Index", "News");
        }

    }
}
