using SocialNet1.Domain.Base;
using Social_Net1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SocialNet1.Infrastructure.Methods;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Social_Net1.Infrastructure.Services.Based
{
    public class ImageService : IMyImage
    {
        private readonly ILogger<ImageService> _logger;

        public ImageService(ILogger<ImageService> logger)
        {
            _logger = logger;
        }

        public byte[] GetSpecialImage(string user)
        {
            string photo = "";

            _logger.LogInformation("Пытаемся разобраться что не так");

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

            var files = Directory.GetFiles("wwwroot/photo/base");

            string s = "";

            foreach (var file in files)
            {
                s += $"{file}\n";
            }

            //_logger.LogInformation(s);

            string photoPath = $"wwwroot/photo/base/{photo}";

            var memorystream = new MemoryStream();

            //_logger.LogInformation("Создали мемори стрим )");

            Image.Load(photoPath);

            using (var newImage = Image.Load(photoPath))
            {
                //_logger.LogInformation("Создали Image )");

                var format = NewImageMethods.GetFormat(photo);

                //_logger.LogInformation($"Сохранили Image по формату {format} )");

                newImage.Save(memorystream, format);
            }

            //_logger.LogInformation("Всё прошло хорошо )");

            var arr = memorystream.ToArray();

            var form = NewImageMethods.GetFormat(arr);

            return arr;
        }


    }
}
