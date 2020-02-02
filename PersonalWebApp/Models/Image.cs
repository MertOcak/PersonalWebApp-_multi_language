using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public string ImagePath { get; set; }
        public ICollection<ProjectImages> ProjectImages { get; set; }
        public ICollection<BlogImages> BlogImages { get; set; }

    }
}
