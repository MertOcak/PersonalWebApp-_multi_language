using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class Category
    {
        [Column("CategoryId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; }
        public bool IsChecked { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relationships Many to Many for Categories
        public ICollection<ProjectCategory> ProjectCategories { get; set; }
        public ICollection<BlogCategories> BlogCategories { get; set; }
    }
}
