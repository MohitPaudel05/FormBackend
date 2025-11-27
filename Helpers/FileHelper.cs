using System.IO;

namespace FormBackend.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> SaveFileAsync(IFormFile file, string folderPath)
        {
            if (file == null || file.Length == 0)
                return null;

            // Ensure folder exists
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Unique file name
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var fullPath = Path.Combine(folderPath, uniqueFileName);

            // Save file to disk
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path to store in DB
            return Path.Combine("images", uniqueFileName).Replace("\\", "/");
        }
    }
    }
