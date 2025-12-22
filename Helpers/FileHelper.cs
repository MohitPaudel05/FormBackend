using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FormBackend.Helpers
{
    public static class FileHelper
    {
        private static readonly string[] permittedImageExtensions = { ".jpg", ".jpeg", ".png" };
        private static readonly string[] permittedDocumentExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };
        private const long maxImageSize = 2_000_000;     // 2 MB
        private const long maxDocumentSize = 5_000_000;  // 5 MB
        private const long maxSignatureSize = 2_000_000; // 2 MB

        /// <summary>
        /// Save a student image to wwwroot/images
        /// </summary>
        public static async Task<string> SaveImageAsync(IFormFile file, string wwwrootPath, string folderName = "images")
        {
            if (file == null || file.Length == 0)
                throw new Exception("No file provided.");

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (Array.IndexOf(permittedImageExtensions, ext) < 0)
                throw new Exception("Invalid file type. Only jpg, jpeg, png allowed.");

            if (file.Length > maxImageSize)
                throw new Exception("File size exceeds 2 MB limit.");

            return await SaveFileAsync(file, wwwrootPath, folderName);
        }

        /// <summary>
        /// Save academic documents to wwwroot/documents
        /// </summary>
        public static async Task<string> SaveDocumentAsync(IFormFile file, string wwwrootPath, string folderName = "documents")
        {
            if (file == null || file.Length == 0)
                throw new Exception("No file provided.");

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (Array.IndexOf(permittedDocumentExtensions, ext) < 0)
                throw new Exception("Invalid file type. Only jpg, jpeg, png, pdf allowed.");

            if (file.Length > maxDocumentSize)
                throw new Exception("File size exceeds 5 MB limit.");

            return await SaveFileAsync(file, wwwrootPath, folderName);
        }

        /// <summary>
        /// Save signatures to wwwroot/signatures
        /// </summary>
        public static async Task<string> SaveSignatureAsync(IFormFile file, string wwwrootPath, string folderName = "signatures")
        {
            if (file == null || file.Length == 0)
                throw new Exception("No file provided.");

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (Array.IndexOf(permittedImageExtensions, ext) < 0)
                throw new Exception("Invalid file type. Only jpg, jpeg, png allowed.");

            if (file.Length > maxSignatureSize)
                throw new Exception("File size exceeds 1 MB limit.");

            return await SaveFileAsync(file, wwwrootPath, folderName);
        }

        /// <summary>
        /// Private helper to save the file physically
        /// </summary>
        private static async Task<string> SaveFileAsync(IFormFile file, string wwwrootPath, string folderName)
        {
            var folderPath = Path.Combine(wwwrootPath, folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path for frontend: "images/abc123.jpg"
            return Path.Combine(folderName, fileName).Replace("\\", "/");
        }
    }
}
