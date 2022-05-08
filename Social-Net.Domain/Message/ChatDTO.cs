using Social_Net.Domain.Base;
using SocialNet1.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Message
{
    /// <summary>
    /// Переписка пользователей
    /// </summary>
    public class ChatDTO : BaseChat
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

        
    }
}
