using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.ViewModels
{
    public class OnePageViewModel
    {
        public IEnumerable<Project> Projects { get; set; }
        public About About { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Experience> Experiences { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public List<Blog> BlogCategories { get; set; }
        public IEnumerable<AboutSkill> AboutSkills { get; set; }
        public ContactRequest ContactRequest { get; set; }
        public General General { get; set; }
        public Page Page { get; set; }
    }
}
