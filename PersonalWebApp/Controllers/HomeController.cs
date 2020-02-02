using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;
using PersonalWebApp.ViewModels;

namespace PersonalWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly IGenericRepository<Project> projectRepository;
        private readonly IGenericRepository<About> aboutRepository;
        private readonly IGenericRepository<Category> categoryRepository;
        private readonly IGenericRepository<Education> educationRepository;
        private readonly IGenericRepository<Skill> skillRepository;
        private readonly IGenericRepository<Experience> experienceRepository;
        private readonly IGenericRepository<Blog> blogRepository;
        private readonly IGenericRepository<AboutSkill> aboutSkillRepository;
        private readonly IGenericRepository<ContactRequest> contactRequestRepository;
        private readonly IGenericRepository<General> generalRepository;
        private readonly IGenericRepository<Page> pageRepository;
        private readonly List<Blog> blogCategories;

        public HomeController(AppDbContext context, DbContextOptions<AppDbContext> contextOptions, IGenericRepository<Project> projectRepository, IGenericRepository<About> aboutRepository, IGenericRepository<Category> categoryRepository, IGenericRepository<Education> educationRepository, IGenericRepository<Skill> skillRepository, IGenericRepository<Experience> experienceRepository, IGenericRepository<Blog> blogRepository, IGenericRepository<AboutSkill> aboutSkillRepository, IGenericRepository<ContactRequest> contactRequestRepository, IGenericRepository<General> generalRepository,IGenericRepository<Page> pageRepository) 
        {
            this.context = context;
            this.contextOptions = contextOptions;
            this.projectRepository = projectRepository;
            this.aboutRepository = aboutRepository;
            this.categoryRepository = categoryRepository;
            this.educationRepository = educationRepository;
            this.skillRepository = skillRepository;
            this.experienceRepository = experienceRepository;
            this.blogRepository = blogRepository;
            this.aboutSkillRepository = aboutSkillRepository;
            this.contactRequestRepository = contactRequestRepository;
            this.generalRepository = generalRepository;
            this.pageRepository = pageRepository;
            using (AppDbContext a = new AppDbContext(contextOptions))
            {
                var blogCategories = a.Blogs.Include(blog => blog.BlogCategories).ThenInclude(blog=>blog.Category).ToList();
                this.blogCategories = blogCategories;
            }
        }
        public IActionResult Index()
        {
            OnePageViewModel onePage = new OnePageViewModel
            {
                Projects = projectRepository.GetAll(),
                About = aboutRepository.GetById(Guid.Parse("3d6257bf-227d-4f35-9d13-369205940242")),
                Categories = categoryRepository.GetAll(),
                Educations = educationRepository.GetAll(),
                Skills = skillRepository.GetAll(),
                Experiences = experienceRepository.GetAll(),
                Blogs = blogRepository.GetAll(),
                BlogCategories = blogCategories,
                AboutSkills = aboutSkillRepository.GetAll(),
                General = generalRepository.GetById(Guid.Parse("fa02f728-016e-44cb-83d8-9dd157d5848b")),
                Page = pageRepository.GetById(Guid.Parse("76ec251b-b7b8-4be6-84df-efb922306ba8"))
                //ContactRequest = new ContactRequest()
            };

            //return RedirectToAction("index", "project",new { area = "panel" });
            return View(onePage);
        }
        [HttpPost]
        public IActionResult saveRequest(OnePageViewModel model)
        {
            if (ModelState.IsValid)
            {
                contactRequestRepository.Insert(model.ContactRequest);
                contactRequestRepository.Save();
                TempData["Succcess"] = "I got your message. Thank you!";
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }
    }
}