using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Infrastructure.Methods
{
    public static class ImageMethods
    {
        public static string GetFormat(byte[] arr)
        {
            string format = "";
            if (arr is not null)
            {
                var memorystream2 = new MemoryStream(arr);
                var im2 = Image.FromStream(memorystream2).RawFormat;

                if (im2 == ImageFormat.Jpeg)
                    format = "jpg";
                else if (im2 == ImageFormat.Png)
                    format = "png";
                else if (im2 == ImageFormat.Gif)
                    format = "gif";
                else
                    format = "jpg";
            }

            return format;
        }

        public static ImageFormat GetFormat(string photoName)
        {
            var photoNames = photoName.Split('.');

            var format = photoNames[photoNames.Length - 1];

            if (format == "jpeg" || format == "jpg") 
                return ImageFormat.Jpeg;

            else if (format == "png" || format == "PNG")
                return ImageFormat.Png;
            else
                throw new ArgumentException("Неправильный формат файла");
        }

        public static bool IsValid(byte[] arr)
        {
            try
            {
                using (var image = Image.FromStream(new MemoryStream(arr)))
                {
                    if (image.RawFormat.Equals(ImageFormat.Jpeg))
                    {
                        return true;
                    }
                    if (image.RawFormat.Equals(ImageFormat.Png))
                    {
                        return true;
                    }
                    if (image.RawFormat.Equals(ImageFormat.Gif))
                    {
                        return true;
                    }

                    return false;
                }

            }
            catch (Exception)
            {

                return false;
            }
        }

        public static byte[] GetByteArrFromFile(IFormFile uploadedFile)
        {
            var im = uploadedFile;
            var memorystream = new MemoryStream();
            im.CopyTo(memorystream);

            var arr = memorystream.ToArray();

            return arr;
        }

        public static byte[] GetByteArrFromFile(string fileName)
        {
            var newImage = Image.FromFile(fileName);
            var memorystream = new MemoryStream();
            newImage.Save(memorystream, ImageFormat.Jpeg);
            var arr = memorystream.ToArray();

            return arr;
        }

        public static string GetStringFromByteArr(byte[] arr) =>
            Convert.ToBase64String(arr);
    }
}
