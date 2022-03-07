using System.ComponentModel.DataAnnotations;

namespace SocialNet1.ViewModels
{
    public class RegisterUserViewModel
    {
        [MaxLength(256)]
        [DataType(DataType.Password)]
        [Display(Name = "Роль пользователя")]
        public string AdminRolePassword { get; set; }

        [Required, MaxLength(256)]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
    }
}
