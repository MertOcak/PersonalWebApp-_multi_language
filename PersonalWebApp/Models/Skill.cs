using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class Skill
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int Dominance { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
