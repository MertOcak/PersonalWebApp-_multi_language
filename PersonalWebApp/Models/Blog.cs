using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class Blog
    {
        public Guid BlogId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        public string Title { get; set; }
        public string PhotoPath { get; set; }
        [Required]
        public string Content { get; set; }
        public ICollection<BlogCategories> BlogCategories { get; set; }
        public ICollection<BlogImages> BlogImages { get; set; }
    }
}
