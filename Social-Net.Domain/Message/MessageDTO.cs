﻿using SocialNet1.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Message
{
    /// <summary>
    /// Сообщение
    /// </summary>
    public class MessageDTO : Entity
    {
        [Required]
        public int HelpId { get; set; }

        /// <summary>
        /// Имя отправителя
        /// </summary>
        [Required]
        public string SenderName { get; set; }

        /// <summary>
        /// Время сообщения
        /// </summary>
        [Required]
        public DateTime TimeMess { get; set; } = DateTime.Now;

        /// <summary>
        /// Содержимый текст
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Сообщение изменено?
        /// </summary>
        [Required]
        public bool IsUpdate { get; set; } = false;


        /// <summary>
        /// Фото
        /// </summary>
        [Required]
        public virtual ICollection<MessageImages> Images { get; set; } = new List<MessageImages>();
    }
}

