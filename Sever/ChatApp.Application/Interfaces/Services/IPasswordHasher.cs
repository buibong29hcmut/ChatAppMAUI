using ChatApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces.Services
{
    public interface IPasswordHasher
    {
        HashPassWordResult HashWithSHA256Algo(string password);
        bool CheckPassWord(string passWordInput, string passWordHash, string salt);
       
    }
}
