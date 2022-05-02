using SocialNet1.Domain.Base;
using SocialNet1.Domain.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Social_Net.Domain.Base
{
    public class BaseChat : Entity
    {
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
