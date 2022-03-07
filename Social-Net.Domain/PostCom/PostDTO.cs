using SocialNet1.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.PostCom
{
    /// <summary>
    /// Запись
    /// </summary>
    public class PostDTO : Entity
    {
        /// <summary>
        /// Данная запись
        /// </summary>
        [Required]
        public virtual CommentDTO ThisPost { get; set; }

        /// <summary>
        /// Комментарии
        /// </summary>
        [Required]
        public virtual ICollection<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
