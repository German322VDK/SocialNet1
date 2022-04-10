using SocialNet1.Domain.PostCom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Base
{
    public class SocNetEntity : Entity
    {
        /// <summary>
        /// Записи пользователя
        /// </summary>
        [Required]
        public virtual ICollection<PostDTO> Posts { get; set; } = new List<PostDTO>();

        /// <summary>
        /// Х координата
        /// </summary>
        [Required]
        public int X { get; set; }

        /// <summary>
        /// У координата
        /// </summary>
        [Required]
        public int Y { get; set; }


        /// <summary>
        /// Номер текущего фото
        /// </summary>
        public int CurrentImage { get; set; } = 0;

        /// <summary>
        /// Работает или удалена
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
