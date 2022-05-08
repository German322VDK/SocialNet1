using System.Collections.Generic;

namespace SocialNet1.Models
{
    public class FullPhotoGroupFormat
    {
        public int ImageId { get; set; }

        public string Format { get; set; }

        public string MainImage { get; set; }

        public int RepostCount { get; set; }

        public PhotoGroupInfoAut Group { get; set; }

        public ICollection<PhotoGroupInfoComms> Comments { get; set; } = new List<PhotoGroupInfoComms>();

        public int Number { get; set; }
    }
}
