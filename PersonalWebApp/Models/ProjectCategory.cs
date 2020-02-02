using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class ProjectCategory
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
