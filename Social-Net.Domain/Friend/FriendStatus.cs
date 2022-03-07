using SocialNet1.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Friends
{
    /// <summary>
    /// Друг пользователя
    /// </summary>
    public class FriendStatus : Entity
    {
        /// <summary>
        /// Имя друга
        /// </summary>
        [Required]
        public string FriendName { get; set; }

        /// <summary>
        /// Статус сокрытия
        /// </summary>
        [Required]
        public bool HiddenStatus { get; set; }
    }
}
