using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;

namespace Model.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.MovieTime).HasMaxLength(15).IsRequired();
            builder.Property(x => x.director).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Premiere).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.ImageUrl).HasMaxLength(300);
            builder.HasOne(x => x.Category).WithMany(x => x.Movies).HasForeignKey(x => x.CategoryId);
        }
    }
}
