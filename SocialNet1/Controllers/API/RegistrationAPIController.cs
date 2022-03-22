using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.Models.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Controllers.API
{
    [Route("api/registr")]
    [ApiController]
    public class RegistrationAPIController : ControllerBase
    {
        private readonly IUser _user;
        private readonly IEmailConfirm _emailConfirm;

        public RegistrationAPIController(IUser user, IEmailConfirm emailConfirm)
        {
            _user = user;
            _emailConfirm = emailConfirm;
        }

        [HttpGet("givehash")]
        public async Task<bool> GiveHash(string email)
        {
            var emails = _user.GetAll().Select(el => el.Email);

            if (email is null || emails.Contains(email) || !SendMailMethods.CheckEmail(email))
                return false;

            var randString =  StringGenerationMethods.Generate(6, engLow:false, EngUp:true, numbers:true);

            var result = _emailConfirm.Set(email, randString);

            if (!result)
                return false;

            await SendMailMethods.SendEmailAsync(Emails.MAIN_EMAIL, Emails.MAIN_NAME, Emails.MAIN_PASS, email, "Подтверждение почты", 
                $"Код для регистрации <b>{randString}</b>. Никому кроме нас его не сообщайте)");

            return true;
        }

        [HttpGet("confirm")]
        public bool Confirm(string email, string hash)
        {
            var ec = _emailConfirm.Get(email);

            if(ec is null)
                return false;

            if(ec.Hash != hash)
                return false;

            return true;
        }

    }
}
