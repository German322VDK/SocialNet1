using SocialNet1.Domain.Base;
using SocialNet1.Domain.Group;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Social_Net.Domain.Clash
{
    public class Side : Entity
    {
        [Required]
        public virtual GroupDTO Group { get; set; }

        [Required]
        public virtual ICollection<ClashLike> GroupLikes { get; set; } = new List<ClashLike>();

        [Required]
        public bool IsReady { get; set; } = false;
    }
}
