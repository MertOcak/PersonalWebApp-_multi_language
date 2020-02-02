using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;

namespace PersonalWebApp.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IGenericRepository<Category> categoryRepository;

        public CategoryController(IGenericRepository<Category> categoryRepository, IHostingEnvironment hostingEnvironment)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(categoryRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("CategoryCreate");
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedAt = DateTime.Now;
                category.UpdatedAt = DateTime.Now;
                categoryRepository.Insert(category);
                categoryRepository.Save();
                TempData["Success"] = "Operation Successful!";
                return RedirectToAction("index");
            }
            return View("CategoryCreate", category);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Category category = categoryRepository.GetById(id);
            return View("CategoryEdit", category);
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            categoryRepository.Delete(id);
            categoryRepository.Save();
            TempData["Success"] = "Operation Successful!";
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedAt = DateTime.Now;
                category.UpdatedAt = DateTime.Now;
                categoryRepository.Update(category);
                categoryRepository.Save();
                TempData["Success"] = "Operation Successful!";
                return RedirectToAction("index");
            }
            else
            {
                return View("ProjectEdit", category);
            }
        }
    }
}