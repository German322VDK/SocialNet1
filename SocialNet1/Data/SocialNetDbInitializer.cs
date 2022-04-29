using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialNet1.Database.SQlite.Context;
using SocialNet1.Domain.Base;
using SocialNet1.Domain.Friends;
using SocialNet1.Domain.Group;
using SocialNet1.Domain.Identity;
using SocialNet1.Domain.Message;
using SocialNet1.Domain.PostCom;
using Social_Net1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Data
{
    public class SocialNetDbInitializer
    {
        private readonly SocialNetDBSQlite _db;
        private readonly ILogger<SocialNetDbInitializer> _logger;
        private readonly UserManager<UserDTO> _userManager;
        private readonly RoleManager<RoleDTO> _roleManager;
        private readonly IMyImage _imageManager;

        private const string _godGroupName = "Главная группа сайта";
        private const string _godShortGroupName = "offgroup";
        private const string _godName = "God";

        public SocialNetDbInitializer(SocialNetDBSQlite db, ILogger<SocialNetDbInitializer> logger,
            UserManager<UserDTO> userManager, RoleManager<RoleDTO> roleManager,
            IMyImage images)
        {
            _db = db;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _imageManager = images;
        }

        public void Initialize()
        {
            var timer = Stopwatch.StartNew();

            using (_logger.BeginScope("Инициализация бд"))
            {
                _logger.LogInformation("Инициализация базы данных...");
            }

            var db = _db.Database;

            if (db.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Выполнение миграций...");

                db.Migrate();

                _logger.LogInformation("Выполнение миграций выполнено успешно");
            }
            else
            {
                _logger.LogInformation("База данных находится в актуальной версии ({0:0.0###} c)",
                    timer.Elapsed.TotalSeconds);
            }

            try
            {
                InitialIdentitiesAsync().Wait();

                InitialGropsAsync().Wait();

                InitialChatsAsync().Wait();

                InitialUserGroupFix();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Ошибка при выполнении инициализации БД :(");
                throw;
            }

            _logger.LogInformation("Инициализация БД выполнена успешно {0}",
                timer.Elapsed.TotalSeconds);

        }

        private async Task InitialIdentitiesAsync()
        {
            var timer = Stopwatch.StartNew();

            _logger.LogInformation("Инициализация системы Identity...");

            await CheckRole(UserStatus.God.ToString());
            await CheckRole(UserStatus.Admin.ToString());
            await CheckRole(UserStatus.User.ToString());
            await CheckRole(UserStatus.Anonymous.ToString());
            await CheckRole(UserStatus.Banned.ToString());

            if (await _userManager.FindByNameAsync(_godName) is null)
            {
                _logger.LogInformation("Отсутствует учётная запись Бога");

                var godImages = new List<UserImages>();

                godImages.Add(new UserImages
                {
                    ImageNumber = 1,
                    Image = _imageManager.GetSpecialImage("GodName")
                });

                var god = new UserDTO
                {
                    FirstName = "Создатель",
                    SecondName = "Сайта",
                    UserName = _godName,
                    Images = godImages,
                    Status = "Обидно что frontent на js, когда мог быть на c#",
                    SocNetItems = new SocNetEntityUser
                    {
                        CurrentImage = 1,
                        X = 2,
                        Y = 2,
                        Posts = new List<PostDTO>()
                    },
                    Friends = new List<FriendStatus>(),
                    Email = "germean322@gmail.com",
                    //Groups = usersGodGroup
                };

                var creation_result = await _userManager.CreateAsync(god, Passwords.God);

                if (creation_result.Succeeded)
                {
                    _logger.LogInformation("Учётная запись Бога создана успешно.");

                    await _userManager.AddToRoleAsync(god, _godName);

                    _logger.LogInformation($"Учётная запись Бога наделена ролью {_godName}");
                }
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description);

                    throw new InvalidOperationException($"Ошибка при создании учётной записи " +
                        $"Бога:( ({string.Join(",", errors)})");
                }
            }

            _logger.LogInformation($"Инициализация системы Identity завершена успешно за " +
                $"{timer.Elapsed.Seconds:0.0##}с");
        }

        private async Task InitialGropsAsync()
        {
            if (_db.Groups.Any())
            {
                _logger.LogInformation("Инициализация БД группами не требуется");
                return;
            }

            _logger.LogInformation("Инициализация групп...");

            using (await _db.Database.BeginTransactionAsync())
            {
                var godGroupName = _godGroupName;

                var godGroupImages = new List<GroupImages>();

                godGroupImages.Add(new GroupImages
                {
                    ImageNumber = 1,
                    Image = _imageManager.GetSpecialImage("GodGroup")
                });

                var godGroup = new GroupDTO
                {
                    GroupName = godGroupName,
                    ShortGroupName = _godShortGroupName,
                    SocNetItems = new SocNetEntityGroup
                    {
                        CurrentImage = 1,
                        X = 2,
                        Y = 2,
                        Posts = new List<PostDTO>()
                    },
                    Images = godGroupImages,
                };

                _db.Groups.Add(godGroup);

                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }

            _logger.LogInformation("Инициализация групп выполнена успешно");
        }

        private async Task InitialChatsAsync()
        {

            if (_db.Chats.Any())
            {
                _logger.LogInformation("Инициализация БД чатами не требуется");
                return;
            }

            _logger.LogInformation("Инициализация чатов...");

            using (await _db.Database.BeginTransactionAsync())
            {
                foreach (var el in _db.Users)
                {
                    var message = new MessageDTO
                    {
                        SenderName = "God",
                        Content = "Hello from God",
                        HelpId = 1
                    };

                    var mas = new List<MessageDTO>();

                    mas.Add(message);

                    _db.Chats.Add(new ChatDTO
                    {
                        UserName1 = "God",
                        UserName2 = el.UserName,
                        Messages = mas,
                    });

                }

                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }

            _logger.LogInformation("Инициализация чатов выполнена успешно");
        }

        private void InitialUserGroupFix()
        {
            var group = _db.Groups.FirstOrDefault(gr => gr.ShortGroupName == _godShortGroupName);
            var user = _db.Users.FirstOrDefault(us => us.UserName == _godName);

            using (_db.Database.BeginTransaction())
            {
                _db.UserGroupStatuses
                    .Add(new UserGroupStatus 
                    { 
                        Group = group,
                        UserDTO = user,
                        Status = Status.Admin,
                        GroupName = _godShortGroupName,
                        UserName = _godName
                    });

                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }
        }

        private async Task CheckRole(string RoleName)
        {
            if (!await _roleManager.RoleExistsAsync(RoleName))
            {
                _logger.LogInformation($"Роль {RoleName} отсуствует. Создаю...");

                await _roleManager.CreateAsync(new RoleDTO
                {
                    Name = RoleName,
                    Description = GetDescription(RoleName)
                });

                _logger.LogInformation($"Роль {RoleName} создана успешно");
            }
        }

        private string GetDescription(string role)
        {
            string desc;

            switch (role)
            {
                case "God":
                    desc = "Может всё";
                    break;
                case "Admin":
                    desc = "Может почти всё";
                    break;
                case "User":
                    desc = "Может что все";
                    break;
                case "Anonymous":
                    desc = "Может почти всё";
                    break;
                case "Banned":
                    desc = "Ничего не может";
                    break;
                default:
                    desc = "Неизвестно";
                    break;
            }

            return desc;
        }

    }
}
