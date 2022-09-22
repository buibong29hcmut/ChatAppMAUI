using ChatApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Test
{
    [TestClass]  
    public class TestUploadFileToAzureBlob:BaseTest
    {
        [TestMethod]
        public async Task UploadFile()
        {
            string filePath = "E:\\240px-Olivine_basalt.jpg";
            using(FileStream stream= File.OpenRead(filePath))
            {
                var formfile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                var fileService = serviceCollectionContainer.
                    ServiceCollection().
                    BuildServiceProvider().
                    GetRequiredService<IUploadFileToAzureBlobService>();
                await fileService.UploadFile(formfile);
            }
            Assert.AreEqual(0, 0);
        }
    }
}
