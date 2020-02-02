using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalWebApp.Models.Configurations;

namespace PersonalWebApp.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<AboutSkill> AboutSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<General> Generals { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<ProjectImages> ProjectImages { get; set; }
        public DbSet<BlogImages> BlogImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\\MSSQLLocalDB;Database=PersonalWebAppDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProjectCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectImageConfiguration());
            modelBuilder.ApplyConfiguration(new BlogCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BlogImageConfiguration());

        }

    }
}
