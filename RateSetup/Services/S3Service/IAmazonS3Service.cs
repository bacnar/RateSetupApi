using Microsoft.AspNetCore.Http;
using RateSetup.Models.FileUpload;
using System;
using System.Threading.Tasks;

namespace RateSetup.Services.S3Service
{
    public interface IAmazonS3Service
    {
        Task<GetObjectModel> GetObject(string name);

        Task<UploadObjectModel> UploadObject(IFormFile file);

        Task<UploadObjectModel> RemoveObject(String fileName);
    }
}
