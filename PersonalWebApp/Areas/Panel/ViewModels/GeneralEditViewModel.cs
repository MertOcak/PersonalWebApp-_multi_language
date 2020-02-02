using Microsoft.AspNetCore.Http;
using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Areas.Panel.ViewModels
{
    public class GeneralEditViewModel
    {
        public GeneralEditViewModel()
        {
            Images = new List<IFormFile>();
        }
        public General Generals { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
