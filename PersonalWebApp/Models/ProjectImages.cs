using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class ProjectImages
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
    }
}
