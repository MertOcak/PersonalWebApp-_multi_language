using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models.Configurations
{
    public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategories>
    {
        public void Configure(EntityTypeBuilder<BlogCategories> builder)
        {
            builder.HasKey(p => new { p.BlogId, p.CategoryId });

            builder.HasOne(pp => pp.Blog)
                .WithMany(p => p.BlogCategories)
                .HasForeignKey(pp => pp.BlogId);

            builder.HasOne(pp => pp.Category)
                .WithMany(p => p.BlogCategories)
                .HasForeignKey(pp => pp.CategoryId);
        }
    }
}
