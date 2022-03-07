using Microsoft.AspNetCore.Identity;
using SocialNet1.Domain.Friends;
using SocialNet1.Domain.Group;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Identity
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserDTO : IdentityUser
    {
        /// <summary>
        /// Первая часть имени
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Вторая часть имени
        /// </summary>
        [Required]
        public string SecondName { get; set; }

        /// <summary>
        /// Собственный статус
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Сосотояния пользователя в группах
        /// </summary>
        [Required]
        public virtual ICollection<UserGroupStatus> Groups { get; set; } = new List<UserGroupStatus>();

        /// <summary>
        /// Посты
        /// Номер текущего фото
        /// Полит координаты
        /// Аккаунт активен/удалён
        /// Дата регистрации
        /// </summary>
        [Required]
        public virtual SocNetEntityUser SocNetItems { get; set; }

        /// <summary>
        /// Фото пользователя
        /// </summary>
        [Required]
        public virtual ICollection<UserImages> Images { get; set; } = new List<UserImages>();

        /// <summary>
        /// Друзья пользователя
        /// </summary>
        [Required]
        public virtual ICollection<FriendStatus> Friends { get; set; } = new List<FriendStatus>();

        
    }
}
