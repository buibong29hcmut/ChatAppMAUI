using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Interfaces.DAL
{
    public interface IChatDbContext
    {
         DbSet<Message> Messages { get; set; }
         DbSet<User> Users { get; set; }
         DbSet<Conversation> Conversations { get; set; }
         Task<int> SaveChangesAndRemoveCacheAsync(CancellationToken cancellationToken = default, params string[] cacheKey);
         Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
         int SaveChangeAndRemoveCache(bool acceptAllChange, params string[] cacheKey);
         int SaveChangeAndRemoveCache(params string[] cacheKey);
    }
}
