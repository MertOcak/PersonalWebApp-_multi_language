using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class Education
    {
        public Guid Id { get; set; }
        [Required]
        public string DegreeName { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
