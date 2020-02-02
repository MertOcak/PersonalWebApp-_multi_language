using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class Project
    {
        [Column("ProjectId")]
        public Guid Id { get; set; }
        [Display(Name = "Project Title", Description = "Please enter project title here")]
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Tag { get; set; }
        public string PhotoPath { get; set; }
        public string ProjectUrl { get; set; }
        public string SefUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relationships Many to Many for Categories
        public ICollection<ProjectCategory> ProjectCategories { get; set; }

        public ICollection<ProjectImages> ProjectImages { get; set; }

    }
}
