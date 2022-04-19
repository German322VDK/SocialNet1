using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Domain.PostCom;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.Models;
using System.Collections.Generic;

namespace SocialNet1.Controllers
{

    [Authorize]
    public class NewsController : Controller
    {
        private IUser _user;
        private readonly ILogger<NewsController> _logger;
        public NewsController(IUser user, ILogger<NewsController> logger)
        {
            _user = user;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            return View();
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
                return RedirectToAction("Index", "News");
            }

            _logger.LogInformation($"Получилось добавить пост: {model.Text} человека {model.UserName} для человека {model.AuthorName}");

            return RedirectToAction("Index", "News");
        }
    }
}
