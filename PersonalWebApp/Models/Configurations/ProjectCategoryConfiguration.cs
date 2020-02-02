using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models.Configurations
{
    public class ProjectCategoryConfiguration : IEntityTypeConfiguration<ProjectCategory>
    {
        public void Configure(EntityTypeBuilder<ProjectCategory> builder)
        {
            builder.HasKey(p => new { p.ProjectId, p.CategoryId });

            builder.HasOne(pp => pp.Project)
                .WithMany(p => p.ProjectCategories)
                .HasForeignKey(pp => pp.ProjectId);

            builder.HasOne(pp => pp.Category)
                .WithMany(p => p.ProjectCategories)
                .HasForeignKey(pp => pp.CategoryId);
        }
    }
}
