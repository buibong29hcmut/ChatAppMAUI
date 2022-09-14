﻿using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Models;
using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace ChatApp.API.Extensions
{
    public static class SeedDbContext
    {
        public static async Task SeedData(this ChatDbContext db,IPasswordHasher _hasher)
        {
            string password = "29122002Az@";
            string UserName = "buibong2912";

        


            var hashPass = _hasher.HashWithSHA256Algo(password);

            User admin = User.CreateRegister(UserName, hashPass.PasswordHash, hashPass.Salt);
            db.Users.Add(admin);
            db.SaveChanges();
            List<User> users = new List<User>();
            for (int i = 0; i < 100; i++)
            {
                var user = User.CreateRegister(UserName + Guid.NewGuid().ToString().Replace('-', '_'), hashPass.PasswordHash, hashPass.Salt);
                users.Add(user);
            }

            await db.AddRangeAsync(users);
            await db.SaveChangesAsync();
            var allUser = db.Users.ToList();
            int cp = 0;
            for (int i = 0; i < allUser.Count; i++)
            {
                List<Conversation> conversations = new List<Conversation>();

                for (int j = 0; j < allUser.Count; j++)
                {
                    if (i == j)
                        continue;
                    if (db.Conversations
                        .Any((p => (p.UserId == allUser[i].Id
                    && p.OtherUserId == allUser[j].Id) || (p.UserId == allUser[j].Id && p.OtherUserId == allUser[i].Id)
                    )))
                    {
                        continue;
                    }
                    var conversation = new Conversation(allUser[i].Id, allUser[j].Id, DateTime.Now);
                    conversations.Add(conversation);
                }
                await db.Conversations.AddRangeAsync(conversations);
                await db.SaveChangesAsync();
                await Task.Delay(100);
            }

            var allConver = await db.Conversations.ToListAsync();
            for (int i = 0; i < allConver.Count; i++)
            {
                List<Message> messages = new();
                for (int j = 0; j < 100; j++)
                {
                    int random = new Random().Next(0, 2);
                    Message message;
                    if (random == 0)
                    {
                        message = new Message(allConver[i].UserId, "The message" + DateTime.Now, DateTimeOffset.Now, allConver[i].Id);
                        messages.Add(message);
                        continue;

                    }
                    message = new Message(allConver[i].OtherUserId, "The message" + DateTime.Now, DateTimeOffset.Now, allConver[i].Id);
                    messages.Add(message);


                }
                await db.AddRangeAsync(messages);
                await db.SaveChangesAsync();
            }

        }
        public static async Task UpdateLastMessageToCache(this ChatDbContext db,IDistributedCache cache)
        {
            var allConversationId= await db.Conversations.ToListAsync();
            int count = 0;
            foreach(var item in allConversationId)
            {
                var message =await db.Messages.Where(p => p.ConversationId == item.Id)
                                       .OrderByDescending(p => p.SendTime)
                                       .FirstOrDefaultAsync();
                item.SetLastMessage(message.Id);
                count++;
                db.SaveChanges();
                Console.WriteLine("Đã update"+count);
            }
        }
    }
}
