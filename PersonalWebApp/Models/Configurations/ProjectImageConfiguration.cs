using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models.Configurations
{
    public class ProjectImageConfiguration : IEntityTypeConfiguration<ProjectImages>
    {
        public void Configure(EntityTypeBuilder<ProjectImages> builder)
        {
            builder.HasKey(p => new { p.ProjectId, p.ImageId });

            builder.HasOne(pp => pp.Project)
                .WithMany(p => p.ProjectImages)
                .HasForeignKey(pp => pp.ProjectId);

            builder.HasOne(pp => pp.Image)
                .WithMany(p => p.ProjectImages)
                .HasForeignKey(pp => pp.ImageId);
        }
    }
}
