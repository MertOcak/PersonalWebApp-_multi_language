using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class BlogImages
    {
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
    }
}
