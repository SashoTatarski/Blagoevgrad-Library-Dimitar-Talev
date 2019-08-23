using Library.Models.Models;
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
                .HasOne(b => b.Book)
                .WithOne(rb => rb.ReservedBook);

            builder
                .HasOne(b => b.User)
                .WithMany(u => u.ReservedBooks)
                .HasForeignKey(x => x.UserId);
        }
    }
}
