using System.IO;

namespace FormBackend.Helpers
{
    public static class FileHelper
    {
        private static readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png" };
        private const long maxFileSize = 1_000_000; // 1 MB

        // wwwrootPath = host.WebRootPath from Program.cs or Controller
        public static async Task<string> SaveImageAsync(IFormFile file, string wwwrootPath, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new Exception("No file provided.");

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (Array.IndexOf(permittedExtensions, ext) < 0)
                throw new Exception("Invalid file type. Only jpg, jpeg, png allowed.");

            if (file.Length > maxFileSize)
                throw new Exception("File size exceeds 1 MB limit.");

            // Ensure folder exists under wwwroot
            var folderPath = Path.Combine(wwwrootPath, folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path for frontend, e.g., "images/filename.jpg"
            return Path.Combine(folderName, fileName).Replace("\\", "/");
        }
    }
    }
