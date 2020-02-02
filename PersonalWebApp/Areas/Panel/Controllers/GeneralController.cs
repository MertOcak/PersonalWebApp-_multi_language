using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalWebApp.Areas.Panel.ViewModels;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;

namespace PersonalWebApp.Areas.Panel.Controllers
{
    [Authorize]
    [Area("Panel")]
    public class GeneralController : Controller
    {
        private readonly AppDbContext context;
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly IGenericRepository<General> generalRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public GeneralController(AppDbContext context, DbContextOptions<AppDbContext> contextOptions, IGenericRepository<General> generalRepository, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.contextOptions = contextOptions;
            this.generalRepository = generalRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            GeneralEditViewModel model = new GeneralEditViewModel
            {
                Generals = generalRepository.GetById(Guid.Parse("fa02f728-016e-44cb-83d8-9dd157d5848b"))
            };

            return View("GeneralEditView", model);
        }
        [HttpPost]
        public IActionResult Index(GeneralEditViewModel model)
        {
            string uniqueFileName = ProcessUploadedFile(model);
            if(uniqueFileName != null)
            {
                model.Generals.SiteLogoPath = uniqueFileName;
            } else
            {
                model.Generals.SiteLogoPath = context.Generals.AsNoTracking().Where(g => g.Id == model.Generals.Id).First().SiteLogoPath;
            }
            generalRepository.Update(model.Generals);
            generalRepository.Save();
            TempData["Success"] = "Changes saved successfully!";
            return View("GeneralEditView", model);
        }
        private string ProcessUploadedFile(GeneralEditViewModel model)
        {
            string uniqueFileName = null;
            if (model.Images != null && model.Images.Count > 0)
            {
                foreach (IFormFile photo in model.Images)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "userdata/general");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }

            }
            return uniqueFileName;
        }

    }
}