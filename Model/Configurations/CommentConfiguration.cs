using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Coments");
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.LastModified).HasColumnType("datetime2").HasPrecision(0);
            builder.Property(x => x.Rating).HasColumnType("decimal").HasPrecision(4,2);
            builder.HasOne(x => x.Movie).WithMany(x => x.Comments).HasForeignKey(x => x.MovieId);
            builder.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId);
        }
    }
}
