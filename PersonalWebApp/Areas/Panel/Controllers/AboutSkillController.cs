using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;

namespace PersonalWebApp.Areas.Panel.Controllers
{
    [Authorize]
    [Area("Panel")]
    public class AboutSkillController : Controller
    {
        private readonly IGenericRepository<AboutSkill> aboutSkillRepository;

        public AboutSkillController(IGenericRepository<AboutSkill> aboutSkillRepository)
        {
            this.aboutSkillRepository = aboutSkillRepository;
        }


        public IActionResult Index()
        {
            return View(aboutSkillRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("AboutSkillCreate");
        }
        [HttpPost]
        public IActionResult Create(AboutSkill model)
        {
            if (ModelState.IsValid)
            {
                aboutSkillRepository.Insert(model);
                aboutSkillRepository.Save();
                return RedirectToAction("Index");
            }
            return View("AboutSkillCreate",model);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            AboutSkill model = aboutSkillRepository.GetById(id);
            return View("AboutSkillEdit", model);
        }
        [HttpPost]
        public IActionResult Edit(AboutSkill model)
        {
            aboutSkillRepository.Update(model);
            aboutSkillRepository.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            aboutSkillRepository.Delete(id);
            aboutSkillRepository.Save();
            return RedirectToAction("Index");
        }
    }
}