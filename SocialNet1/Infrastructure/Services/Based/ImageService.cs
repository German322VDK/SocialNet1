using SocialNet1.Domain.Base;
using Social_Net1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SocialNet1.Infrastructure.Methods;

namespace Social_Net1.Infrastructure.Services.Based
{
    public class ImageService : IImage
    {
        public byte[] GetSpecialImage(string user)
        {
            string photo = "";

            switch (user)
            {
                case "GodName":
                    photo = "pepegod.jpg";
                    break;
                case "GodGroup":
                    photo = "godgroup.jpeg";
                    break;
                default:
                    throw new NullReferenceException("Не корректное название фото");
            }

            string photoPath = $"wwwroot/photo/base/{photo}";

            var memorystream = new MemoryStream();

            var newImage = Image.FromFile(photoPath);

            newImage.Save(memorystream, ImageMethods.GetFormat(photo));

            return memorystream.ToArray();
        }


    }
}
