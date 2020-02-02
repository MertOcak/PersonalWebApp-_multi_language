using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class Experience
    {
        public Guid Id { get; set; }
        [Required]
        public string ExperienceName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Icon { get; set; }
    }
}
