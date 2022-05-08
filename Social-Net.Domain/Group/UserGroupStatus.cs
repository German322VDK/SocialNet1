using SocialNet1.Domain.Base;
using SocialNet1.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Group
{
    /// <summary>
    /// Связь группы и пользователя
    /// </summary>
    public class UserGroupStatus : Entity
    {
        /// <summary>
        /// Статус члена группы
        /// </summary>
        [Required]
        public Status Status { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Имя группы
        /// </summary>
        [Required]
        public string GroupName { get; set; }

        [Required]
        public virtual GroupDTO Group { get; set; }

        [Required]
        public virtual UserDTO UserDTO { get; set; }
    }
}
