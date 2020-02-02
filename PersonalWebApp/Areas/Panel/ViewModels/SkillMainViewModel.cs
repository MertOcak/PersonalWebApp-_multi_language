using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Areas.Panel.ViewModels
{
    public class SkillMainViewModel
    {
        public IEnumerable<Skill> Skills { get; set; }
        public Page Page { get; set; }
    }
}
