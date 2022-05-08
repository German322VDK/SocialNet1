using Microsoft.AspNetCore.Http;

namespace SocialNet1.Models
{
    public class AddGroupPhotoModel
    {
        public string GroupName { get; set; }

        public IFormFile uploadedFile { get; set; }
    }
}
