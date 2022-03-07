using SocialNet1.Domain.Base;
using Social_Net1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Net1.Infrastructure.Services.Based
{
    public class ImageService : IImage
    {
        public byte[] GetSpecialImage(string user)
        {
            return GetBytePhoto(user);
        }

        private byte[] GetBytePhoto(string photoName)
        {

            string photo = "";

            switch (photoName)
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

            newImage.Save(memorystream, GetFormat(photo));

            return memorystream.ToArray();
        }

        private ImageFormat GetFormat(string photoName)
        {
            var photoNames = photoName.Split('.');

            var format = photoNames[photoNames.Length - 1];

            if (format == "jpeg" || format == "jpg")
            {
                return ImageFormat.Jpeg;
            }
            else if (format == "png" || format == "PNG")
            {
                return ImageFormat.Png;
            }
            else
            {
                throw new ArgumentException("Неправильный формат файла");
            }
        }

    }
}
