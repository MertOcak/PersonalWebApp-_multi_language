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
    [Area("Panel")]
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly AppDbContext context;
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly IGenericRepository<Category> categoryRepository;
        private readonly IGenericRepository<Project> projectRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public ProjectController(AppDbContext context, DbContextOptions<AppDbContext> contextOptions, IGenericRepository<Category> categoryRepository, IGenericRepository<Project> projectRepository, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.contextOptions = contextOptions;
            this.categoryRepository = categoryRepository;
            this.projectRepository = projectRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ProjectListViewModel data = new ProjectListViewModel();
            data.Projects = projectRepository.GetAll();
            data.Categories = categoryRepository.GetAll();
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ProjectEditViewModel data = new ProjectEditViewModel();
            data.Categories = categoryRepository.GetAll();
            return View("ProjectCreate", data);
        }
        [HttpPost]
        public IActionResult Create(ProjectEditViewModel data, string[] categoryList)
        {
            if (ModelState.IsValid)
            {
                Project project = new Project();
                project.Id = Guid.NewGuid();
                project.Title = data.Project.Title;
                project.Description = data.Project.Description;
                project.CreatedAt = DateTime.Now;
                project.UpdatedAt = DateTime.Now;
                projectRepository.Insert(project);
                projectRepository.Save();
                using (var a = new AppDbContext(contextOptions))
                {
                    for (int i = 0; i < categoryList.Length; i++)
                    {
                        var category = a.Categories
                        .Single(p => p.Id == Guid.Parse(categoryList[i]));

                        a.Projects.Include(p => p.ProjectCategories).Single(p => p.Id == project.Id).ProjectCategories.Add(new ProjectCategory
                        {
                            Project = project,
                            Category = category
                        });
                        a.SaveChanges();
                    }
                }

                if (data.Images.Any())
                {
                    string uniqueFileName = ProcessUploadedFile(data, project);
                    project.PhotoPath = uniqueFileName;
                }
                else
                {
                    project.PhotoPath = null;
                }
                projectRepository.Update(project);
                projectRepository.Save();
                TempData["Success"] = "Operation Successful!";
                return RedirectToAction("index");
            }
            else
            {
                return View("ProjectCreate", data);
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            ProjectEditViewModel data = new ProjectEditViewModel();
            data.Categories = categoryRepository.GetAll();

            var categoriesImport = context.Projects
   .Include(x => x.ProjectCategories).ThenInclude(x => x.Category);

            var selectedCategories = categoriesImport.Where(x => x.Id == id).Select(x => x.ProjectCategories).First().ToList();

            for (int i = 0; i < selectedCategories.Count(); i++)
            {
                foreach (var category in data.Categories)
                {
                    if (category.Id == selectedCategories[i].CategoryId)
                    {
                        category.IsChecked = true;
                    }
                }
            }

            var images = context.Projects.Include(p => p.ProjectImages).ThenInclude(p => p.Image).ToList();

            List<string> projectImages = new List<string>();
            foreach (Project p in images)
            {
                var filter = p.ProjectImages;

                foreach (var pi in filter)
                {
                    if (pi.ProjectId == id)
                    {
                        projectImages.Add(pi.Image.ImagePath);
                    }
                }
            }

            data.ImagePath = projectImages;


            //var allProjects = context.Projects.Include(p => p.ProjectCategories).ToList();

            //   List<Project> categoryRelatedProjects = new List<Project>();
            //   foreach (Project p in allProjects)
            //   {
            //       var filter = p.ProjectCategories;

            //       foreach (var pr in filter)
            //       {
            //           if (pr.CategoryId.ToString() == "83278a7e-2a50-4914-62c9-08d79ca3d529")
            //           {
            //               categoryRelatedProjects.Add(p);
            //           }
            //       }
            //   }



            data.Project = projectRepository.GetById(id);
            return View("ProjectEdit", data);
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            projectRepository.Delete(id);
            projectRepository.Save();
            TempData["Success"] = "Operation Successful!";
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult Update(ProjectEditViewModel data, Guid[] categoryList)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(data);

                Project project = new Project();
                project.Id = data.Project.Id;
                project.Title = data.Project.Title;
                project.Description = data.Project.Description;
                project.UpdatedAt = DateTime.Now;
                if (uniqueFileName != null)
                {
                    project.PhotoPath = uniqueFileName;
                }
                else
                {
                    project.PhotoPath = context.Projects.AsNoTracking().Where(p => p.Id == data.Project.Id).First().PhotoPath;
                }

                projectRepository.Update(project);
                projectRepository.Save();

                using (var a = new AppDbContext(contextOptions))
                {

                    var currentProject = a.Projects.Include(p => p.ProjectCategories).Single(p => p.Id == data.Project.Id);


                    //foreach (var category in currentProject.ProjectCategories)
                    //{
                    //    currentProject.ProjectCategories.Remove(category);
                    //}
                    //context.SaveChanges();


                    //Delete Categories
                    foreach (var item in currentProject.ProjectCategories.ToList())
                    {
                        currentProject.ProjectCategories.Remove(item);
                        a.SaveChanges();
                    }

                    for (int i = 0; i < categoryList.Length; i++)
                    {

                        var category = a.Categories
                        .Single(p => p.Id == categoryList[i]);



                        //bool exists = context.Entry(category)
                        // .Collection(m => m.ProjectCategories)
                        // .Query()
                        // .Any(x => x.CategoryId == categoryList[i]);



                        //if (exists)
                        //    continue;



                        // Add Categories
                        currentProject.ProjectCategories.Add(new ProjectCategory
                        {
                            Project = currentProject,
                            Category = category
                        });
                        a.SaveChanges();
                    }

                }

                TempData["Success"] = "Operation Successful!";
                return RedirectToAction("index");
            }
            else
            {
                return View("ProjectEdit", data);
            }
        }
        private string ProcessUploadedFile(ProjectEditViewModel model)
        {
            string uniqueFileName = null;
            if (model.Images != null && model.Images.Count > 0)
            {
                foreach (IFormFile photo in model.Images)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "userdata/projects");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }

                    var currentProject = context.Projects.AsNoTracking().Include(p => p.ProjectCategories).Single(p => p.Id == model.Project.Id);


                    Image image = new Image();
                    image.ImageId = Guid.NewGuid();
                    image.ImagePath = uniqueFileName;

                    ProjectImages newImage = new ProjectImages();
                    newImage.Project = currentProject;
                    newImage.Image = image;

                    using (var a = new AppDbContext(contextOptions))
                    {
                        a.Projects.Include(p => p.ProjectImages).Single(p => p.Id == model.Project.Id).ProjectImages.Add(newImage);
                        a.SaveChanges();
                    }

                }

            }

            return uniqueFileName;
        }
        private string ProcessUploadedFile(ProjectEditViewModel model, Project project)
        {
            string uniqueFileName = null;
            if (model.Images != null && model.Images.Count > 0)
            {
                foreach (IFormFile photo in model.Images)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "userdata/projects");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }

                    Image image = new Image();
                    image.ImageId = Guid.NewGuid();
                    image.ImagePath = uniqueFileName;

                    ProjectImages newImage = new ProjectImages();
                    newImage.Project = project;
                    newImage.Image = image;

                    context.ProjectImages.Add(newImage);
                    context.SaveChanges();

                }

            }

            return uniqueFileName;
        }
    }
}