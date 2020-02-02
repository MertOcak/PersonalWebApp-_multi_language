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
    public class ExperienceController : Controller
    {
        private readonly IGenericRepository<Experience> experienceRepository;

        public ExperienceController(IGenericRepository<Experience> experienceRepository)
        {
            this.experienceRepository = experienceRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(experienceRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("ExperienceCreate");
        }
        [HttpPost]
        public IActionResult Create(Experience model)
        {
            if (ModelState.IsValid)
            {
                experienceRepository.Insert(model);
                experienceRepository.Save();
                TempData["Success"] = "Operation Successful!";
                return RedirectToAction("Index");
            }
            return View("ExperienceCreate", model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Experience model = experienceRepository.GetById(id);
            return View("ExperienceEdit", model);
        }
        [HttpPost]
        public IActionResult Edit(Experience model)
        {
            if (ModelState.IsValid)
            {
                experienceRepository.Update(model);
                experienceRepository.Save();
                return RedirectToAction("Index");
            }
            return View("ExperienceEdit", model);
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            experienceRepository.Delete(id);
            experienceRepository.Save();
            TempData["Success"] = "Operation Successful!";
            return RedirectToAction("Index");
        }

    }
}