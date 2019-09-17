using Library.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Database.Configurations
{
    public class BannedUserConfiguration : IEntityTypeConfiguration<BannedUser>
    {
        public void Configure(EntityTypeBuilder<BannedUser> builder)
        {
            builder
                .HasKey(bu => bu.UserId);

            builder
                .HasOne(bu => bu.User)
                .WithOne(u => u.BannedUser);              
        }
    }
}
