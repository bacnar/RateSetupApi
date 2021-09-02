using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RateSetup.Helpers.ConfigurationDTOs;
using RateSetup.Models.FileUpload;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RateSetup.Services.S3Service
{
    public class AmazonS3Service : IAmazonS3Service
    {
        private readonly S3Settings _s3Settings;
        private readonly AmazonS3Client _amazonS3Client;

        public AmazonS3Service(IOptions<S3Settings> s3Settings)
        {
            _s3Settings = s3Settings.Value;

            var s3ClientConfig = new AmazonS3Config
            {
                ServiceURL = _s3Settings.Url
            };

            _amazonS3Client = new AmazonS3Client(s3Settings.Value.AccessKey, s3Settings.Value.AccessSecret, s3ClientConfig);
        }

        public async Task<GetObjectModel> GetObject(string name)
        {
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = _s3Settings.Bucket,
                Key = name
            };
            var responseObject = await _amazonS3Client.GetObjectAsync(request);
            return new GetObjectModel
            {

                ContentType = responseObject.Headers.ContentType,
                Content = responseObject.ResponseStream
            };
        }

        public async Task<UploadObjectModel> UploadObject(IFormFile file)
        {

            byte[] fileBytes = new Byte[file.Length];
            file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

            var fileName = Guid.NewGuid() + file.FileName;

            PutObjectResponse response = null;

            using (var stream = new MemoryStream(fileBytes))
            {
                var request = new PutObjectRequest
                {
                    BucketName = _s3Settings.Bucket,
                    Key = fileName,
                    InputStream = stream,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                response = await _amazonS3Client.PutObjectAsync(request);
            };

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                // this model is up to you, in my case I have to use it following;
                return new UploadObjectModel
                {
                    Success = true,
                    FileName = fileName
                };
            }
            else
            {
                return new UploadObjectModel
                {
                    Success = false,
                    FileName = fileName
                };
            }
        }

        public async Task<UploadObjectModel> RemoveObject(String fileName)
        {

            var request = new DeleteObjectRequest
            {
                BucketName = _s3Settings.Bucket,
                Key = fileName
            };

            var response = await _amazonS3Client.DeleteObjectAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return new UploadObjectModel
                {
                    Success = true,
                    FileName = fileName
                };
            }
            else
            {
                return new UploadObjectModel
                {
                    Success = false,
                    FileName = fileName
                };
            }
        }
    }
}
