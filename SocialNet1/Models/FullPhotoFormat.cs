using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Models
{
    public class FullPhotoFormat
    {
        public int ImageId { get; set; }

        public string Format { get; set; }

        public string MainImage { get; set; }

        public int RepostCount { get; set; }

        public PhotoUserInfo Auhor { get; set; }

        public ICollection<PhotoUserInfo> Comments { get; set; } = new List<PhotoUserInfo>();
    }
}
