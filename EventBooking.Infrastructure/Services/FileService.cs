using EventBooking.Application.Common.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace EventBooking.Infrastructure.Services
{
    public class FileService : IFileService
    {
        //IWebHostEnvironment  Cung cấp thông tin về môi trường máy chủ
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task<string> UploadImageAsync(IFormFile file, string folderPath)
        {
            // Check xem file ảnh có hợp lệ hay không
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Không có tệp nào được tải lên.");
            }
            // Tạo đường dẫn
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            // tạo tên tệp bằng GUID tránh trùng lặp
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            // Kiểm tra và tạo thư mục nếu chưa tồn tại.
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            // Sao chép tệp vào đường dẫn đã xác định bằng cách sử dụng FileStream
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }

        public async Task DeleteImageAsync(string imageName, string folderPath)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath, imageName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            await Task.CompletedTask;
        }

    }
}
