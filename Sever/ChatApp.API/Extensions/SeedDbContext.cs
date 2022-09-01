using ChatApp.Application.Interfaces.Services;
using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Contexts;

namespace ChatApp.API.Extensions
{
    public static class SeedDbContext
    {
        public static async Task SeedData(this ChatDbContext _db,IPasswordHasher _hasher)
        {
            // List<User> Users=new List<User>();
            //string passWord = "218826Az@";
            //var hashResult= _hasher.HashWithSHA256Algo(passWord);
            // for(int i = 0; i < 100; i++)
            //{
            //    User user = User.CreateRegister("buibong2912", 
            //        hashResult.PasswordHash, hashResult.Salt,
            //        "Bui Bong"
            //        +new Guid().ToString().Substring(0,4));
            //    Users.Add(user);    

            //}
            //await _db.Users.AddRangeAsync(Users);
            //await _db.SaveChangesAsync();
            var allUsers = _db.Users.ToList();
            //foreach(var user in allUsers)
            //{
            //    user.UserName = user.UserName + Guid.NewGuid().ToString();
            //    user.Name = user.Name + Guid.NewGuid().ToString();
            //    _db.SaveChanges();
            //    await Task.Delay(200);

            //}
            var allMesss = _db.Messages.ToList();
            _db.RemoveRange(allMesss);
            await _db.SaveChangesAsync();
            await Task.Delay(200);
            for(int i = 0; i < allUsers.Count; i++)
            {
                List<Message> Messages = new List<Message>();
                for (int j = 0; j < 1000; j++) 
                {
                    for(int k = 0; k < allUsers.Count; k++)
                    {
                        if (i == k)
                            continue;
                        var message = new Message(allUsers[i].Id, allUsers[k].Id, "Message"+Guid.NewGuid().ToString().Substring(0,6), DateTime.UtcNow);
                        Messages.Add(message);
                      

                    }
                  
                }
                await _db.AddRangeAsync(Messages);
                await Task.Delay(100);
                await _db.SaveChangesAsync();
                await Task.Delay(200);
            }
            Console.WriteLine("Done");

        }
    }
}
