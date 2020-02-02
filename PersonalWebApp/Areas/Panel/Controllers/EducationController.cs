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
    public class EducationController : Controller
    {
        private readonly AppDbContext context;
        private readonly IGenericRepository<Education> educationRepository;

        public EducationController(AppDbContext context, IGenericRepository<Education> educationRepository)
        {
            this.context = context;
            this.educationRepository = educationRepository;
        }
        public IActionResult Index()
        {
            return View(educationRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("EducationCreate");
        }

        [HttpPost]
        public IActionResult Create(Education model)
        {
            if (ModelState.IsValid)
            {
                educationRepository.Insert(model);
                educationRepository.Save();
                TempData["Success"] = "Operation Successful!";
                return RedirectToAction("Index");
            }

            return View("EducationCreate", model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Education model = educationRepository.GetById(id);
            return View("EducationEdit", model);
        }

        [HttpPost]
        public IActionResult Edit(Education model)
        {
            educationRepository.Update(model);
            educationRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            educationRepository.Delete(id);
            educationRepository.Save();
            return RedirectToAction("Index");
        }


    }
}