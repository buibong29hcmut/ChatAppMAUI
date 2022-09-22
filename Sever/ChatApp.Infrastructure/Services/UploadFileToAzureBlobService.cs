using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ChatApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Services
{
    public class UploadFileToAzureBlobService:IUploadFileToAzureBlobService
    {
        private readonly IConfiguration _configuration;
        public UploadFileToAzureBlobService( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> UploadFile(IFormFile file)
        {

                string connectionString = _configuration.GetValue<string>("AzureBlob");
                using (Stream stream = file.OpenReadStream())
                {
                 string fileNameInBlob=  Guid.NewGuid().ToString();
                 BlobClient _blobClient = new BlobClient(connectionString, "useravt", fileNameInBlob+"."+GetExtension(file.FileName));

                 var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpeg" };

                    var response = await _blobClient.UploadAsync(stream,new BlobUploadOptions()
                    {
                        HttpHeaders = blobHttpHeader
                    });
                 return _blobClient.Uri.ToString();
                }
        }
        private string GetExtension(string attachment_name)
        {
            var index_point = attachment_name.IndexOf(".") + 1;
            return attachment_name.Substring(index_point);
        }
    }
}
