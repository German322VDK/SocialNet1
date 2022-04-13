using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using System;
using System.IO;

namespace SocialNet1.Infrastructure.Methods
{
    public class NewImageMethods
    {
        public static string GetFormat(byte[] arr)
        {
            string format = "";
            if (arr is not null)
            {
                var im = Image.DetectFormat(arr);

                if (im.Name == "JPEG")
                    format = "jpg";
                else if (im.Name == "PNG")
                    format = "png";
                else if (im.Name == "GIF")
                    format = "gif";
                else
                    format = "";
            }

            return format;
        }

        public static IImageEncoder GetFormat(string photoName)
        {
            var photoNames = photoName.Split('.');

            var format = photoNames[photoNames.Length - 1];

            if (format == "jpeg" || format == "jpg")
                return new JpegEncoder();

            else if (format == "png" || format == "PNG")
                return new PngEncoder();
            else
                throw new ArgumentException("Неправильный формат файла");
        }

        public static bool IsValid(byte[] arr)
        {
            try
            {
                var fm = GetFormat(arr);

                if (string.IsNullOrEmpty(fm))
                    return false;
                else
                    return true;
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
            var memorystream = new MemoryStream();

            using (var newImage = Image.Load(fileName))
            {
                newImage.Save(memorystream, GetFormat(fileName));
            }

            var arr = memorystream.ToArray();

            return arr;
        }

        public static string GetStringFromByteArr(byte[] arr) =>
            Convert.ToBase64String(arr);

    }
}
