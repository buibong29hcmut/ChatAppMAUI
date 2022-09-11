using ChatApp.Application.Interfaces.Services;
using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Contexts;
using ChatApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

string connectionString = "Server=localhost:5432;Port=5432;Database=ChatDb;User Id=admin;Password=admin1234";
DbContextOptions options = new DbContextOptionsBuilder<ChatDbContext>()
                         .UseNpgsql(connectionString)
                         .EnableSensitiveDataLogging(true)
                         .Options;
ChatDbContext db = new ChatDbContext(options,null);
string password = "29122002Az@";
string UserName = "buibong2912";
IPasswordHasher PasswordHasher = new PasswordHasher();
var hashPass = PasswordHasher.HashWithSHA256Algo(password);
var allUser2 = db.Users.ToList();
db.RemoveRange(allUser2);
db.SaveChanges();
User admin = User.CreateRegister(UserName, hashPass.PasswordHash, hashPass.Salt);
db.Users.Add(admin);
db.SaveChanges();
List<User> users = new List<User>();
for (int i = 0; i < 1000; i++)
{
    var user = User.CreateRegister(UserName + Guid.NewGuid().ToString().Replace('-', '_'), hashPass.PasswordHash, hashPass.Salt);
    users.Add(user);
}

await db.AddRangeAsync(users);
await db.SaveChangesAsync();
var allUser = db.Users.ToList();
List<Conversation> conversations = new List<Conversation>();
int co = 0;
for (int i = 0; i < allUser.Count; i++)
{
    for (int j = 0; j < allUser.Count; j++)
    {
        if (i == j)
            continue;
        var conversation = new Conversation(allUser[i].Id, allUser[j].Id, DateTime.Now);
        conversations.Add(conversation);
        Console.WriteLine(co++);
    }

}
await db.Conversations.AddRangeAsync(conversations);
await db.SaveChangesAsync();
var allConver = await db.Conversations.ToListAsync();
for (int i = 0; i < allConver.Count; i++)
{
    List<Message> messages = new();
    for (int j = 0; j < 10000; j++)
    {
        int random = new Random().Next(0, 2);
        Message message;
        if (random == 0)
        {
            await Task.Delay(500);
            message = new Message(allConver[i].UserId, "The message" + DateTime.Now, DateTimeOffset.Now, allConver[i].Id);
            messages.Add(message);
            continue;

        }
        message = new Message(allConver[i].OtherUserId, "The message" + DateTime.Now, DateTimeOffset.Now, allConver[i].Id);
        messages.Add(message);


    }
    await db.AddRangeAsync(messages);
    await db.SaveChangesAsync();
    await Task.Delay(100);
}
Console.WriteLine("Done");
