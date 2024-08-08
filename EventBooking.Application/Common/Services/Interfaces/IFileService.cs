using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Services.Interfaces
{
    public interface IFileService
    {
        //List<ImportDataDto> ReadFromCsv(Stream fileStream);
        //List<ImportDataDto> ReadFromExcel(Stream fileStream);
        Task<string> UploadImageAsync(IFormFile file, string folderPath);
        Task DeleteImageAsync(string imageName, string folderPath);
    }
}
