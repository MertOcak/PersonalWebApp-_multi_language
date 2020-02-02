using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Areas.Panel.ViewModels
{
    public class ProjectListViewModel
    {
        public ProjectListViewModel()
        {
            Projects = new List<Project>();
            Categories = new List<Category>();
        }

        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
