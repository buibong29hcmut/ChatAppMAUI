using ChatApp.Application.Interfaces.Services;
using ChatApp.Infrastructure.Services;

IPasswordHasher hasher = new PasswordHasher();
var result= hasher.HashWithSHA256Algo("29122002Az@");
var valider = hasher.CheckPassWord("29122002Az@", result.PasswordHash, result.Salt);
Console.WriteLine(valider.ToString());
Console.WriteLine("Hello, World!");
