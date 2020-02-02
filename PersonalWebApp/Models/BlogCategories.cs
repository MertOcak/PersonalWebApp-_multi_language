using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class BlogCategories
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
