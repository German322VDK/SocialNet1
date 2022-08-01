using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.ViewModels
{
    public class AddGroupViewModel
    {
        [HiddenInput]
        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Короткое имя группы обязательно и не должено использоваться другими"), MaxLength(256)]
        [Display(Name = "Короткое имя групп")]
        [RegularExpression(@"^[A-Za-z0-9]{1,40}$",
         ErrorMessage = "Только английские буквы и цыфры")]
        public string ShortGroupName { get; set; }

        [Required(ErrorMessage = "Название группы обязателено"), MaxLength(256)]
        [Display(Name = "Название группы")]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "Фотография группы обязательна")]
        public IFormFile UploadedFile { get; set; }

        [Required]
        //[Range(-2, 2, ErrorMessage = "X = {-2, 2}")]
        [Range(-2, 2)]
        public int X { get; set; } = 0;

        [Required]
        //[Range(-2, 2, ErrorMessage = "X = {-2, 2}")]
        [Range(-2, 2)]
        public int Y { get; set; } = 0;
    }
}
