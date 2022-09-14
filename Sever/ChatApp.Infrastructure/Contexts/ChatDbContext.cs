using ChatApp.Application.Interfaces.DAL;
using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Contexts
{
    public class ChatDbContext:DbContext, IChatDbContext
    {
        private readonly IDistributedCache _cache;
        public ChatDbContext(DbContextOptions options,
            IDistributedCache cache)
            : base(options)
        {
            _cache = cache;
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MessageConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
       
        public async Task<int> SaveChangesAndRemoveCacheAsync( CancellationToken cancellationToken=default, params string[] cacheKey)
        {
            for(int i = 0; i < cacheKey.Length; i++)
            {
              await  _cache.RemoveAsync(cacheKey[i]);
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        public int SaveChangeAndRemoveCache(bool acceptAllChange, params string[] cacheKey)
        {
            for (int i = 0; i < cacheKey.Length; i++)
            {
                _cache.Remove(cacheKey[i]);
            }
            return base.SaveChanges(acceptAllChange);
        }
        public int SaveChangeAndRemoveCache( params string[] cacheKey)
        {
            for (int i = 0; i < cacheKey.Length; i++)
            {
                 _cache.Remove(cacheKey[i]);
            }
            return  base.SaveChanges();
        }
    }
}
