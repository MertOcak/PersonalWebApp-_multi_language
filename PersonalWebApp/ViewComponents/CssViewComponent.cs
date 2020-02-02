using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.ViewComponents
{
    public class CssViewComponent : ViewComponent
    {
        private readonly IGenericRepository<Skill> skillRepository;

        public CssViewComponent(IGenericRepository<Skill> skillRepository)
        {
            this.skillRepository = skillRepository;
        }
        public IViewComponentResult Invoke()
        {
            return View(skillRepository.GetAll());
        }
    }
}
