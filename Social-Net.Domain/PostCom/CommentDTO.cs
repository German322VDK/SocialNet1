using SocialNet1.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.PostCom
{
    /// <summary>
    /// Комментарий
    /// </summary>
    public class CommentDTO : Entity
    {
        /// <summary>
        /// Имя комментатора
        /// </summary>
        [Required]
        public string CommentatorName { get; set; }

        /// <summary>
        /// Состояние комментатора (группа или пользователь )
        /// </summary>
        [Required]
        public CommentatorStatus CommentatorStatus { get; set; }

        /// <summary>
        /// Время создание записи
        /// </summary>
        [Required]
        public DateTime DateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Содержимый текст
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Фото
        /// </summary>
        [Required]
        public virtual ICollection<CommentImages> Images { get; set; } = new List<CommentImages>();

        /// <summary>
        /// Лайки (Имя лайкнувшего)
        /// </summary>
        [Required]
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
