using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNet1.Domain.Base
{
    /// <summary>
    /// Сущность
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Уникальный Идентефикатор
        /// </summary>
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
