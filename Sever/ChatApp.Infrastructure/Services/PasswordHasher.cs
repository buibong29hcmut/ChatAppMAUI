using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace ChatApp.Infrastructure.Services
{
    public class PasswordHasher:IPasswordHasher
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public HashPassWordResult HashWithSHA256Algo(string password)
        {
            var salt = GenerateRandomSalt();
            return HashWithSaltSHA256(password,Convert.ToBase64String(salt));
        }

        public bool CheckPassWord(string passWordInput, string passWordHash,string salt)
        {
            var newPasswordHash = GeneratePassWordHashSHA256(passWordInput, salt);
            return newPasswordHash.Equals(passWordHash);
        }
        private byte[] GenerateSalt(int Keylength)
        {            
                byte[] randomBytes = new byte[Keylength];
                rngCsp.GetBytes(randomBytes);
                return randomBytes;

        }
        private string GeneratePassWordHashSHA256(string passWordInput,string salt)
        {
            return GeneratePassWordHash(passWordInput, salt, new SHA256Managed());
        }
        private string GeneratePassWordHash(string passWordInput, string salt, HashAlgorithm hashAlgo)
        {
            byte[] passWordBytes = Encoding.UTF8.GetBytes(passWordInput + salt);
            byte[] passWordHash = hashAlgo.ComputeHash(passWordBytes);
            return Convert.ToBase64String(passWordHash);

        }
        private HashPassWordResult HashWithSalt(string passwordInput, string salt, HashAlgorithm hashAlgo)
        {
            var passWordHash = GeneratePassWordHash(passwordInput, salt, hashAlgo);
            return new HashPassWordResult(salt, passWordHash);
        }
        private HashPassWordResult HashWithSaltSHA256(string password, string salt)
        {
            return HashWithSalt(password, salt, new SHA256Managed());
        }
        private byte[] GenerateRandomSalt()
        {
            return GenerateSalt(new Random().Next(10, 20));
        }
    }
}
