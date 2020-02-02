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
    public class BlogController : Controller
    {
        private readonly AppDbContext context;
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly IGenericRepository<Category> categoryRepository;
        private readonly IGenericRepository<Blog> blogRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public BlogController(AppDbContext context, DbContextOptions<AppDbContext> contextOptions, IGenericRepository<Category> categoryRepository, IGenericRepository<Blog> blogRepository, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this.contextOptions = contextOptions;
            this.categoryRepository = categoryRepository;
            this.blogRepository = blogRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(blogRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            BlogEditViewModel data = new BlogEditViewModel();
            data.Categories = categoryRepository.GetAll();
            return View("BlogCreate", data);
        }
        [HttpPost]
        public IActionResult Create(BlogEditViewModel data, string[] categoryList)
        {
            if (ModelState.IsValid)
            {
                Blog blog = new Blog();
                blog.BlogId = Guid.NewGuid();
                blog.Title = data.Blog.Title;
                blog.Content = data.Blog.Content;
                blog.CreatedAt = DateTime.Now;
                blog.UpdatedAt = DateTime.Now;
                blogRepository.Insert(blog);
                blogRepository.Save();
                using (var a = new AppDbContext(contextOptions))
                {
                    for (int i = 0; i < categoryList.Length; i++)
                    {
                        var category = a.Categories
                        .Single(p => p.Id == Guid.Parse(categoryList[i]));

                        a.Blogs.Include(p => p.BlogCategories).Single(p => p.BlogId == blog.BlogId).BlogCategories.Add(new BlogCategories
                        {
                            Blog = blog,
                            Category = category
                        });
                        a.SaveChanges();
                    }
                }

                if (data.Images.Any())
                {
                    string uniqueFileName = ProcessUploadedFile(data, blog);
                    blog.PhotoPath = uniqueFileName;
                }
                else
                {
                    blog.PhotoPath = null;
                }
                blogRepository.Update(blog);
                blogRepository.Save();
                TempData["Success"] = "Operation Successful!";
                return RedirectToAction("index");
            }
            else
            {
                return View("BlogCreate", data);
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            BlogEditViewModel data = new BlogEditViewModel();
            data.Categories = categoryRepository.GetAll();

            var categoriesImport = context.Blogs
   .Include(x => x.BlogCategories).ThenInclude(x => x.Category);

            var selectedCategories = categoriesImport.Where(x => x.BlogId == id).Select(x => x.BlogCategories).First().ToList();

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

            var images = context.Blogs.Include(p => p.BlogImages).ThenInclude(p => p.Image).ToList();

            List<string> blogImages = new List<string>();
            foreach (Blog p in images)
            {
                var filter = p.BlogImages;

                foreach (var pi in filter)
                {
                    if (pi.BlogId == id)
                    {
                        blogImages.Add(pi.Image.ImagePath);
                    }
                }
            }

            data.ImagePath = blogImages;


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



            data.Blog = blogRepository.GetById(id);
            return View("BlogEdit", data);
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            blogRepository.Delete(id);
            blogRepository.Save();
            TempData["Success"] = "Operation Successful!";
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult Update(BlogEditViewModel data, Guid[] categoryList)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(data);

                Blog blog = new Blog();
                blog.BlogId = data.Blog.BlogId;
                blog.Title = data.Blog.Title;
                blog.Content = data.Blog.Content;
                blog.UpdatedAt = DateTime.Now;
                if (uniqueFileName != null)
                {
                    blog.PhotoPath = uniqueFileName;
                }
                else
                {
                    blog.PhotoPath = context.Blogs.AsNoTracking().Where(p => p.BlogId == data.Blog.BlogId).First().PhotoPath;
                }

                blogRepository.Update(blog);
                blogRepository.Save();

                using (var a = new AppDbContext(contextOptions))
                {

                    var currentBlog = a.Blogs.Include(p => p.BlogCategories).Single(p => p.BlogId == data.Blog.BlogId);


                    //foreach (var category in currentProject.ProjectCategories)
                    //{
                    //    currentProject.ProjectCategories.Remove(category);
                    //}
                    //context.SaveChanges();


                    //Delete Categories
                    foreach (var item in currentBlog.BlogCategories.ToList())
                    {
                        currentBlog.BlogCategories.Remove(item);
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
                        currentBlog.BlogCategories.Add(new BlogCategories
                        {
                            Blog = currentBlog,
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
        private string ProcessUploadedFile(BlogEditViewModel model)
        {
            string uniqueFileName = null;
            if (model.Images != null && model.Images.Count > 0)
            {
                foreach (IFormFile photo in model.Images)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "userdata/blogs");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }

                    var currentBlog = context.Blogs.AsNoTracking().Include(p => p.BlogCategories).Single(p => p.BlogId == model.Blog.BlogId);


                    Image image = new Image();
                    image.ImageId = Guid.NewGuid();
                    image.ImagePath = uniqueFileName;

                    BlogImages newImage = new BlogImages();
                    newImage.Blog = currentBlog;
                    newImage.Image = image;

                    using (var a = new AppDbContext(contextOptions))
                    {
                        a.Blogs.Include(p => p.BlogImages).Single(p => p.BlogId == model.Blog.BlogId).BlogImages.Add(newImage);
                        a.SaveChanges();
                    }

                }

            }

            return uniqueFileName;
        }
        private string ProcessUploadedFile(BlogEditViewModel model, Blog blog)
        {
            string uniqueFileName = null;
            if (model.Images != null && model.Images.Count > 0)
            {
                foreach (IFormFile photo in model.Images)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "userdata/blogs");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }

                    Image image = new Image();
                    image.ImageId = Guid.NewGuid();
                    image.ImagePath = uniqueFileName;

                    BlogImages newImage = new BlogImages();
                    newImage.Blog = blog;
                    newImage.Image = image;

                    //context.BlogImages.Add(newImage);
                    context.SaveChanges();

                }

            }

            return uniqueFileName;
        }
    }
}