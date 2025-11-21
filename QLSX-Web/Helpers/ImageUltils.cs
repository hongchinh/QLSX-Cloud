using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Web.Services
{
    public class ImageUltils
    {
        private const int ImageMinimumBytes = 512000;

        public static async Task<string> DataToBase64(BlazorInputFile.IFileListEntry fileItem)
        {
            using (var reader = new StreamReader(fileItem.Data))
            {
                using (var memStream = new MemoryStream())
                {
                    await reader.BaseStream.CopyToAsync(memStream);
                    var fileData = memStream.ToArray();
                    var encodedBase64 = Convert.ToBase64String(fileData);

                    return encodedBase64;
                }
            }
        }

        public static string IsValidImage(string pictureName, string pictureBase64)
        {
            if (string.IsNullOrEmpty(pictureBase64))
            {
                return "File not found!";
            }
            var fileData = Convert.FromBase64String(pictureBase64);

            if (fileData.Length <= 0)
            {
                return "File length is 0!";
            }

            //if (fileData.Length > ImageMinimumBytes)
            //{
            //    return "Maximum length is 512KB";
            //}

            if (!IsExtensionValid(pictureName))
            {
                return "File is not image";
            }

            return null;
        }
        private static bool IsExtensionValid(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            return string.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(extension, ".gif", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(extension, ".svg", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase);
        }
    }
}
