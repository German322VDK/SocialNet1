using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SocialNet1.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private IUser _user;
        private IGroup _group;
        private readonly ILogger<GroupController> _logger;

        public GroupController(IUser user, IGroup group, ILogger<GroupController> logger)
        {
            _user = user;
            _group = group;
            _logger = logger;
        }

        public IActionResult Index(string userName = null)
        {
            if (_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");

            }
            string username;

            if (userName is not null)
            {
                username = userName;
            }
            else
            {
                username = User.Identity.Name;
            }

            var groups = _user.Get(username).Groups.Select(el => el.GroupName).ToArray();

            var userGroups = _group.Get(groups);

            var gropsVM = new List<GroupViewModel>();

            foreach (var item in userGroups)
            {
                int x = item.SocNetItems.X, y = item.SocNetItems.Y;

                var arr = item.Images.FirstOrDefault(im => im.ImageNumber == item.SocNetItems.CurrentImage).Image;

                var group = new GroupViewModel 
                { 
                    MainName = item.GroupName,
                    MainShortName = item.ShortGroupName,
                    UserCount = item.Users.Count,
                    CoordImage = $"photo/coordinates/{x}d{y}.jpg",
                    MainImage = NewImageMethods.GetStringFromByteArr(arr),
                    MainFormat = NewImageMethods.GetFormat(arr)
                };

                gropsVM.Add(group);
            }

            return View(gropsVM);
        }

        public IActionResult Group(string groupName)
        {
            if (_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrEmpty(groupName))
            {
                _logger.LogWarning($"Имя группы не пришло");

                return RedirectToAction("Error", "Home");
            }

            var group = _group.Get(groupName);

            if(group is null)
            {
                _logger.LogWarning($"Группа {groupName} потерялась");

                return RedirectToAction("Error", "Home");
            }

            var arr = group.Images.FirstOrDefault(im => im.ImageNumber == group.SocNetItems.CurrentImage).Image;
            int x = group.SocNetItems.X, y = group.SocNetItems.Y;


            var groupVM = new GroupItemViewModel
            {
                MainName = group.GroupName,
                MainShortName = group.ShortGroupName,
                CoordImage = $"photo/coordinates/{x}d{y}.jpg",
                MainImage = NewImageMethods.GetStringFromByteArr(arr),
                MainFormat = NewImageMethods.GetFormat(arr),
                Images = group.Images
            };

            return View(groupVM);
        }

    }
}
