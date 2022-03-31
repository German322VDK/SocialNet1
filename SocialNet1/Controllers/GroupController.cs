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
                    MainImage = ImageMethods.GetStringFromByteArr(arr),
                    MainFormat = ImageMethods.GetFormat(arr)
                };

                gropsVM.Add(group);
            }

            return View(gropsVM);
        }

        public IActionResult Group(string groupName)
        {
            return View();
        }

    }
}
