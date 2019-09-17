﻿using Library.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Database.Configurations
{
    class ReservedBookConfiguration : IEntityTypeConfiguration<ReservedBook>
    {
        public void Configure(EntityTypeBuilder<ReservedBook> builder)
        {
            builder
               .HasKey(rb => rb.BookId);

            builder
                .HasOne(resBook => resBook.Book)
                .WithMany(book => book.ReservedBooks);

            builder
                .HasOne(resBook => resBook.User)
                .WithMany(u => u.ReservedBooks)
                .HasForeignKey(resBook => resBook.UserId);
        }
    }
}
