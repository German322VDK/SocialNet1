using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.ViewModels
{
    public class RegisterUserViewModel
    {
        [HiddenInput]
        public string Email { get; set; }

        [MaxLength(256)]
        [DataType(DataType.Password)]
        [Display(Name = "Роль пользователя")]
        public string AdminRolePassword { get; set; }

        [Required(ErrorMessage = "Логин обязателен и не должен использоваться другими"), MaxLength(256)]
        [Display(Name = "Логин пользователя")]
        [RegularExpression(@"^[A-Za-z0-9]{1,40}$",
         ErrorMessage = "Только английские буквы и цыфры")]
        //[Remote(action: "CheckUserName", controller: "Account", ErrorMessage = "Email уже используется")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Имя пользователя обязателено"), MaxLength(256)]
        [Display(Name = "Имя пользователя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия пользователя обязателена"), MaxLength(256)]
        [Display(Name = "Фамилия пользователя")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтверждение пароля обязательно")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпали")]
        public string PasswordConfirm { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true")]
        public bool IsConfirmed { get; set; } = false;
    }
}
