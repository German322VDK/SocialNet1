using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialNet1.Database.SQlite.Context;
using SocialNet1.Domain.Identity;
using Social_Net1.Infrastructure.Interfaces.Based;
using Social_Net1.Infrastructure.Middleware;
using Social_Net1.Infrastructure.Providers;
using Social_Net1.Infrastructure.Services.Based;
using SocialNet1.Data;
using SocialNet1.Hubs;
using System;
using SocialNet1.Infrastructure.Services.Admin;
using SocialNet1.Infrastructure.Interfaces.Admin;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Services.Based;

namespace SocialNet1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SocialNetDBSQlite>(opt => opt
               .UseSqlite(Configuration.GetConnectionString("Sqlite"))
               .UseLazyLoadingProxies()
           //opt.UseSqlServer(Configuration.GetConnectionString("SqlServer"))
           //.UseLazyLoadingProxies()
           );


            services.AddIdentity<UserDTO, RoleDTO>()
                .AddEntityFrameworkStores<SocialNetDBSQlite>()
                .AddDefaultTokenProviders();

            services.AddTransient<SocialNetDbInitializer>();

            services.AddTransient<IMyImage, ImageService>();
            services.AddTransient<IUser, UserService>();
            services.AddTransient<IFriends, FriendsService>(); 
            services.AddTransient<IChat, ChatService>(); 
            services.AddTransient<IGroup, GroupService>(); 
            services.AddTransient<IEmailConfirm, EmailConfirmService>();

            services.AddTransient<IUserIdProvider, CustomUserIdProvider>();

            services.AddTransient<ILogInfo, LogInfoService>();

            services.AddSignalR();

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;

//#if DEBUG
//                opt.Password.RequiredLength = 3;
//                opt.Password.RequireDigit = false;
//                opt.Password.RequireLowercase = false;
//                opt.Password.RequireUppercase = false;
//                opt.Password.RequireNonAlphanumeric = false;
//                opt.Password.RequiredUniqueChars = 3;
//#endif

//                opt.User.RequireUniqueEmail = false;

//                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
//                opt.Lockout.AllowedForNewUsers = false;
//                opt.Lockout.MaxFailedAccessAttempts = 10;
//                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "SocialNetCookie";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
             SocialNetDbInitializer db)
        {
            db.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOnlineUsers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/chat");

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=News}/{action=Index}/{id?}"
                );

            });

        }
    }
}
