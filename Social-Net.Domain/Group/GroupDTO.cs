using SocialNet1.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Group
{
    /// <summary>
    /// Группа
    /// </summary>
    public class GroupDTO : Entity
    {
        /// <summary>
        /// Имя Группы
        /// </summary>
        [Required]
        public string GroupName { get; set; }

        [Required]
        public string ShortGroupName { get; set; }

        /// <summary>
         /// Члены группы
        /// </summary>
        [Required]
        public virtual ICollection<UserGroupStatus> Users { get; set; } = new List<UserGroupStatus>();

        [Required]
        public virtual SocNetEntityGroup SocNetItems { get; set; }

        [Required]
        public virtual ICollection<GroupImages> Images { get; set; } = new List<GroupImages>();

    }
}
