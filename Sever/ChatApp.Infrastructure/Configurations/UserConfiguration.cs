using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(b => b.Id)
                .IsRequired();
            builder.Property(b => b.UserName).HasMaxLength(100);
            builder.Property(b => b.PhoneNumber).HasMaxLength(12);
            builder.Property(b => b.Password).HasMaxLength(100);
            builder.Property(b => b.UrlAvatar).HasMaxLength(200);
            builder.Property(b=>b.Email).HasMaxLength(200);
            builder.Property(b => b.Salt).HasMaxLength(200);
            builder.Property(b => b.Name).HasMaxLength(200);
         
        }
    }
}
