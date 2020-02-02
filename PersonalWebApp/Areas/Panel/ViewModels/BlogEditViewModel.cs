using Microsoft.AspNetCore.Http;
using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Areas.Panel.ViewModels
{
    public class BlogEditViewModel
    {
        public BlogEditViewModel()
        {
            Categories = new List<Category>();
            Images = new List<IFormFile>();
            ImagePath = new List<string>();
        }

        public Blog Blog { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public List<IFormFile> Images { get; set; }

        public List<string> ImagePath { get; set; }

    }
}
