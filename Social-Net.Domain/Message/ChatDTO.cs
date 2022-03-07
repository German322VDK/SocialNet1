using SocialNet1.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Message
{
    /// <summary>
    /// Переписка пользователей
    /// </summary>
    public class ChatDTO : Entity
    {
        /// <summary>
        /// Первый пользователь
        /// </summary>
        [Required]
        public string UserName1 { get; set; }

        /// <summary>
        /// Второй пользователь
        /// </summary>
        [Required]
        public string UserName2 { get; set; }

        /// <summary>
        /// Сообщения
        /// </summary>
        [Required]
        public virtual ICollection<MessageDTO> Messages { get; set; } = new List<MessageDTO>();

        /// <summary>
        /// Время последнего сообщения
        /// </summary>
        [Required]
        public DateTime LastTimeMess { get; set; } = DateTime.Now;
    }
}
