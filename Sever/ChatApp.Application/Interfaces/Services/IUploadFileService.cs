using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces.Services
{
    public interface IUploadFileService
    {
        Task<string> UploadFile(IFormFile file);

    }
}
