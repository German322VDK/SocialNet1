using Microsoft.AspNetCore.Http;

namespace SocialNet1.Models
{
    public class ComModel
    {
        public string AuthorName { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public IFormFile uploadedFile { get; set; }
    }
}
