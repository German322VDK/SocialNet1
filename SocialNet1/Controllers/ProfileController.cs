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
            var currantUserName = User.Identity!.Name;

            _logger.LogInformation($"{currantUserName} пытается добавить фотографию");

            if (uploadedFile is null)
            {
                _logger.LogWarning($"Фото не пришло(");

                return RedirectToAction("Index", "News");
            }
                

            var arr = ImageMethods.GetByteArrFromFile(uploadedFile);

            var result = ImageMethods.IsValid(arr);

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

            if(userName is null)
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
            if(model is null || model.UserName is null || model.AuthorName is null || model.Text is null)
            {
                _logger.LogWarning("Данные для добавления поста не пришли");

                return RedirectToAction("Index", "Profile");
            }

            var result = _user.AddUserPost(model.AuthorName, new PostDTO 
            { 
                ThisPost = new CommentDTO
                {
                    CommentatorStatus = CommentatorStatus.User,
                    CommentatorName = model.UserName,
                    Content = model.Text,
                }
            });

            if (!result)
            {
                _logger.LogWarning($"Не получилось добавить пост: {model.Text} человека {model.UserName} для человека {model.AuthorName}");
                return RedirectToAction("Index", "Profile");
            }

            _logger.LogInformation($"Получилось добавить пост: {model.Text} человека {model.UserName} для человека {model.AuthorName}");

            return RedirectToAction("Index", "Profile", new { userName = model.AuthorName });
        }

    }
}
