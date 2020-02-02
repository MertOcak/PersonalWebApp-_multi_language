using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;

namespace PersonalWebApp.Areas.Panel.Controllers
{
    [Authorize]
    [Area("Panel")]
    public class AboutController : Controller
    {
        private readonly AppDbContext context;
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly IGenericRepository<About> aboutRepository;

        public AboutController(AppDbContext context, DbContextOptions<AppDbContext> contextOptions, IGenericRepository<About> aboutRepository)
        {
            this.context = context;
            this.contextOptions = contextOptions;
            this.aboutRepository = aboutRepository;
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            About model = aboutRepository.GetById(id);
            return View("AboutEdit", model);
        }


        [HttpPost]
        public IActionResult Edit(About model)
        {
            if (ModelState.IsValid)
            {
                aboutRepository.Update(model);
                aboutRepository.Save();
                TempData["Success"] = "Changes Saved";
                return RedirectToAction("Edit", new { id = model.Id });
            }
            return View("AboutEdit", model);
        }

    }
}