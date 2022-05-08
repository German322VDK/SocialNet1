using Social_Net.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Social_Net.Domain.Clash
{
    public class ClashDTO : BaseChat
    {
        [Required]
        public virtual Side Side1 { get; set; }

        [Required]
        public virtual Side Side2 { get; set; }
    }
}
