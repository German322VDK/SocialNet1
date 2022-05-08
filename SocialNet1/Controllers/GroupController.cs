using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using SocialNet1.Domain.Group;
using SocialNet1.Domain.PostCom;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.Models;
using SocialNet1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNet1.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private IUser _user;
        private IGroup _group;
        private IClash _clash;
        private readonly ILogger<GroupController> _logger;

        public GroupController(IUser user, IGroup group, IClash clash, ILogger<GroupController> logger)
        {
            _user = user;
            _group = group;
            _clash = clash;
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

            var userAdminGroups = userGroups
                .Where(gr => gr.Users.Where(gu => gu.Status == Status.Admin).Select(u => u.UserName).Contains(User.Identity.Name))
                .ToList();

            var gropsVM = new List<GroupViewModel>();

            var groupsAdmins = new List<GroupViewModel>();

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

            foreach (var item in userAdminGroups)
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

                groupsAdmins.Add(group);
            }

            _logger.LogInformation($"Тип заходит {User.Identity.Name} в группы типа {username}");

            return View(new GroupsFullViewModel 
            { 
                AllGroups = gropsVM,
                Admins = groupsAdmins,
                IsAuthor = username == User.Identity.Name
            });
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
                Images = group.Images,
                Group = group
            };

            _logger.LogInformation($"Тип заходит {User.Identity.Name} в группу {groupName}");

            return View(groupVM);
        }

        public IActionResult GroupSubs(string groupName)
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

            if (group is null)
            {
                _logger.LogWarning($"Группа {groupName} потерялась");

                return RedirectToAction("Error", "Home");
            }

            var users = group.Users.Select(us => us.UserDTO);

            var usersData = new List<GroupUserModel>();

            foreach (var user in users)
            {
                var arr = user.Images.FirstOrDefault(im => im.ImageNumber == user.SocNetItems.CurrentImage).Image;

                var str = NewImageMethods.GetStringFromByteArr(arr);

                var format = NewImageMethods.GetFormat(arr);

                var x = user.SocNetItems.X;
                var y = user.SocNetItems.Y;

                usersData.Add(new GroupUserModel
                {
                    AuthorFormat = format,
                    AuthorImage = str,
                    AuthorCoordinatesImage = $"/photo/coordinates/{x}d{y}.jpg",
                    Color = UserMethods.GetColor(x, y),
                    AuthorUserName = user.UserName,
                    AuthorFirstName = user.FirstName,
                    AuthorSecondName = user.SecondName,
                    FriendsCount = user.Friends.Count
                });
            }

            usersData = usersData.OrderBy(el => el.AuthorSecondName).ToList();

            return View(usersData);
        }

        public IActionResult AddGroup() =>
            View(new AddGroupViewModel { UserName = User.Identity.Name});

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddGroup(AddGroupViewModel model)
        {
            GroupDTO group = null;

            if (model is not null && !string.IsNullOrEmpty(model.ShortGroupName) )
            {
                group = _group.Get(model.ShortGroupName);

                if (group is not null)
                {
                    _logger.LogWarning($"Группа {model.ShortGroupName} занята(");

                    ModelState["ShortGroupName"].Errors.Add(new Exception("Короткое имя группы обязательно и не должено использоваться другими"));
                    ModelState["ShortGroupName"].ValidationState = ModelValidationState.Invalid;
                }

                
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Данные повреждены, не получилось добавить группу(");

                return View(model);
            }

            var arr = NewImageMethods.GetByteArrFromFile(model.UploadedFile);

            if (!NewImageMethods.IsValid(arr))
            {
                _logger.LogWarning($"Фото для группы {model.ShortGroupName} чела {model.UserName} повреждено, не получилось добавить группу(");

                return View(model);
            }

            var result = _group.Add(model.ShortGroupName, model.GroupName, model.UserName, model.X, model.Y, arr);

            if (!result)
            {
                _logger.LogWarning($"Не получилось добавить группу {model.ShortGroupName} чела {model.UserName}(");

                return RedirectToAction("Index", "Group");
            }
            else
            {
                _logger.LogInformation($"Получилось добавить группу {model.ShortGroupName} чела {model.UserName}");

                return RedirectToAction("Group", "Group", new { groupName = model.ShortGroupName });
            }

        }

        public IActionResult GroupManagement(string groupName)
        {
            var user = _user.Get(User.Identity.Name);

            if (user is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");

            }

            var IsUserGroupAdmin = _group.IsUserAdmin(groupName, user.UserName);

            if (!IsUserGroupAdmin)
            {
                _logger.LogWarning($"Чел {user.UserName} не админ группы {groupName} (");

                return RedirectToAction("Error", "Home");
            }

            var thisGroup = _group.Get(groupName);

            var readyGroups = _clash.GetReadyGroups(groupName).Select(gr => _group.Get(gr));

            var groups = _group.GetAll();

            groups.Remove(thisGroup);

            foreach (var item in readyGroups)
            {
                groups.Remove(item);
            }

            var reqGropsVM = new List<GroupViewModel>();
            var allGropsVM = new List<GroupViewModel>();

            foreach (var item in readyGroups)
            {
                int x = item.SocNetItems.X, y = item.SocNetItems.Y;

                var arr = item.Images.FirstOrDefault(im => im.ImageNumber == item.SocNetItems.CurrentImage).Image;

                var group = new GroupViewModel
                {
                    MainName = item.GroupName,
                    MainShortName = item.ShortGroupName,
                    UserCount = item.Users.Count,
                    CoordImage = $"/photo/coordinates/{x}d{y}.jpg",
                    MainImage = NewImageMethods.GetStringFromByteArr(arr),
                    MainFormat = NewImageMethods.GetFormat(arr)
                };

                reqGropsVM.Add(group);
            }

            foreach (var item in groups)
            {
                int x = item.SocNetItems.X, y = item.SocNetItems.Y;

                var arr = item.Images.FirstOrDefault(im => im.ImageNumber == item.SocNetItems.CurrentImage).Image;

                var group = new GroupViewModel
                {
                    MainName = item.GroupName,
                    MainShortName = item.ShortGroupName,
                    UserCount = item.Users.Count,
                    CoordImage = $"/photo/coordinates/{x}d{y}.jpg",
                    MainImage = NewImageMethods.GetStringFromByteArr(arr),
                    MainFormat = NewImageMethods.GetFormat(arr)
                };

                allGropsVM.Add(group);
            }

            _logger.LogInformation($"Чел {user.UserName} заходит в управление группой {groupName}");

            return View(new GroupManagementViewModel 
            {
                Requests = reqGropsVM,
                All = allGropsVM,
                ThisGroupName = groupName
            });
        }

        public IActionResult AddClash(string thisGroupName, string groupName)
        {
            var user = _user.Get(User.Identity.Name);

            if (user is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");

            }

            var thisGroup = _group.Get(thisGroupName);
            var group = _group.Get(groupName);

            if(thisGroup is null || group is null)
            {
                _logger.LogWarning($"Группы {thisGroupName} или группы {groupName} не существует(");

                return RedirectToAction("Error", "Home");
            }

            var IsUserGroupAdmin = _group.IsUserAdmin(thisGroupName, user.UserName);

            if (!IsUserGroupAdmin)
            {
                _logger.LogWarning($"Чел {user.UserName} не админ группы {thisGroupName} (");

                return RedirectToAction("Error", "Home");
            }

            var result = _clash.Add(thisGroupName, groupName);

            if (!result)
            {
                _logger.LogWarning($"Не получилось создать противостояние групп {thisGroupName} и {groupName} (");
            }
            else
            {
                _logger.LogInformation($"Получилось создать противостояние групп {thisGroupName} и {groupName}");
            }

            return RedirectToAction("GroupManagement", "Group", new { groupName = thisGroupName });
        }

        public IActionResult ConfirmClash(string thisGroupName, string groupName)
        {
            var user = _user.Get(User.Identity.Name);

            if (user is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");

            }

            var thisGroup = _group.Get(thisGroupName);
            var group = _group.Get(groupName);

            if (thisGroup is null || group is null)
            {
                _logger.LogWarning($"Группы {thisGroupName} или группы {groupName} не существует(");

                return RedirectToAction("Error", "Home");
            }

            var IsUserGroupAdmin = _group.IsUserAdmin(thisGroupName, user.UserName);

            if (!IsUserGroupAdmin)
            {
                _logger.LogWarning($"Чел {user.UserName} не админ группы {thisGroupName} (");

                return RedirectToAction("Error", "Home");
            }

            var result = _clash.Confirm(thisGroupName, groupName);

            if (!result)
            {
                _logger.LogWarning($"Не получилось подтвердить противостояние групп {thisGroupName} и {groupName} (");
            }
            else
            {
                _logger.LogInformation($"Получилось подтвердить противостояние групп {thisGroupName} и {groupName}");
            }

            return RedirectToAction("GroupManagement", "Group", new { groupName = thisGroupName });
        }

        public IActionResult Delete(string groupName)
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

            if (group is null)
            {
                _logger.LogWarning($"Группа {groupName} потерялась");

                return RedirectToAction("Error", "Home");
            }

            var result = _group.Delete(groupName);

            if (!result)
            {
                _logger.LogWarning($"Не получилось удалить группу {groupName} чела {User.Identity.Name} (");
            }
            else
            {
                _logger.LogInformation($"Получилось удалить группу {groupName} чела {User.Identity.Name}");
            }

            return RedirectToAction("Index", "Group");
        }

        public IActionResult AddImage(AddGroupPhotoModel model)
        {
            if (_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            var currantUserName = User.Identity!.Name;

            if(string.IsNullOrEmpty(model.GroupName) || _group.Get(model.GroupName) is null)
            {
                _logger.LogWarning($"{currantUserName} не понятно в какую группу добавлять фотографию");
            }

            _logger.LogInformation($"{currantUserName} пытается добавить фотографию в группу {model.GroupName}");

            if (model.uploadedFile is null)
            {
                _logger.LogWarning($"Фото не пришло(");

                return RedirectToAction("Group", "Group", new { groupName = model.GroupName });
            }


            var arr = NewImageMethods.GetByteArrFromFile(model.uploadedFile);

            var result = NewImageMethods.IsValid(arr);

            if (!result)
            {
                _logger.LogWarning($"Фото плохое(");

                return RedirectToAction("Index", "News");
            }

            var resultAdding = _group.AddPhoto(arr, model.GroupName);

            if (!resultAdding)
            {
                _logger.LogWarning($"Фото почему то не добавилось(");

                return RedirectToAction("Index", "News");
            }

            _logger.LogInformation($"{currantUserName} успешно добавил фотографию в группу {model.GroupName}");

            return RedirectToAction("Group", "Group", new { groupName = model.GroupName});
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

                return RedirectToAction("Index", "Group");
            }

            PostDTO post;
            string text = string.IsNullOrEmpty(model.Text) ? "" : model.Text;

            if (model.uploadedFile is not null)
            {

                var arr = NewImageMethods.GetByteArrFromFile(model.uploadedFile);

                var resultArr = NewImageMethods.IsValid(arr);

                if (resultArr)
                {
                    _logger.LogInformation($"Получилось добавить пост с картинкой: {text} человека {model.UserName} для группы {model.AuthorName}");

                    post = new PostDTO
                    {
                        ThisPost = new CommentDTO
                        {
                            CommentatorStatus = CommentatorStatus.User,
                            CommentatorName = model.AuthorName,
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
                    _logger.LogWarning($"Не получилось добавить пост с картинкой: {text} человека {model.UserName} для группы {model.AuthorName}");
                    post = new PostDTO
                    {
                        ThisPost = new CommentDTO
                        {
                            CommentatorStatus = CommentatorStatus.User,
                            CommentatorName = model.AuthorName,
                            Content = text
                        }
                    };
                }
            }
            else
            {
                _logger.LogInformation($"Получилось добавить пост без картинкой: {text} человека {model.UserName} для группы {model.AuthorName}");

                post = new PostDTO
                {
                    ThisPost = new CommentDTO
                    {
                        CommentatorStatus = CommentatorStatus.User,
                        CommentatorName = model.AuthorName,
                        Content = text
                    }
                };
            }

            var result = _group.AddPost(model.AuthorName, post);

            if (!result)
            {
                _logger.LogWarning($"Не получилось добавить пост: {model.Text} человека {model.UserName} для группы {model.AuthorName}");

                return RedirectToAction("Group", "Group", new { groupName = model.AuthorName });
            }

            _logger.LogInformation($"Получилось добавить пост: {model.Text} человека {model.UserName} для группы {model.AuthorName}");

            return RedirectToAction("Group", "Group", new { groupName = model.AuthorName });
        }

        public IActionResult SetAva(string groupName, int ava)
        {
            if (_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrEmpty(groupName))
            {
                _logger.LogWarning($"Не понятно кому менять аву(");

                return RedirectToAction("Group", "Group", new { GroupName = groupName });
            }

            var result = _group.SetAva(ava, groupName);

            if (!result)
            {
                _logger.LogWarning($"Группа {groupName} не смог выбрать авой фото № {ava}");

                return RedirectToAction("Group", "Group", new { GroupName = groupName });
            }

            _logger.LogInformation($"Группа {groupName} смог выбрать авой фото № {ava}");

            return RedirectToAction("Group", "Group", new { GroupName = groupName });
        }

        public IActionResult SetCoord(string groupName, int x, int y)
        {
            var user = _user.Get(User.Identity.Name);

            if (user is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            

            if (string.IsNullOrEmpty(groupName))
            {
                _logger.LogWarning($"Не понятно кому менять взгляды(");

                return RedirectToAction("Index", "Profile");
            }

            var result = _group.SetCoord(groupName, x, y);

            if (!result)
            {
                _logger.LogWarning($"Человек {user.UserName} не смог выбрать взгляды группы {groupName} x:{x};y:{y}");

                return RedirectToAction("Group", "Group", new { groupName  = groupName });
            }

            _logger.LogInformation($"Человек {user.UserName} смог выбрать взгляды группы {groupName} x:{x};y:{y}");

            return RedirectToAction("Group", "Group", new { groupName = groupName });
        }

    }
}
