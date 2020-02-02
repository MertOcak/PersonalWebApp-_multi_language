using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Areas.Panel.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly IGenericRepository<About> aboutRepository;

        public MainMenuViewComponent(IGenericRepository<About> aboutRepository)
        {
            this.aboutRepository = aboutRepository;
        }

        public IViewComponentResult Invoke() => View(aboutRepository.GetById(Guid.Parse("3d6257bf-227d-4f35-9d13-369205940242")));
    }
}
