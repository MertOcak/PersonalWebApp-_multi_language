using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models.Configurations
{
    public class BlogImageConfiguration : IEntityTypeConfiguration<BlogImages>
    {
        public void Configure(EntityTypeBuilder<BlogImages> builder)
        {
            builder.HasKey(p => new { p.BlogId, p.ImageId });

            builder.HasOne(pp => pp.Blog)
                .WithMany(p => p.BlogImages)
                .HasForeignKey(pp => pp.BlogId);

            builder.HasOne(pp => pp.Image)
                .WithMany(p => p.BlogImages)
                .HasForeignKey(pp => pp.ImageId);
        }
    }
}
