using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Areas.Admin.ViewModels;
using PersonalWebApp.Models;

namespace PersonalWebApp.Areas.Panel.Controllers
{
    [Area("Panel")]
    [Authorize]
    public class PanelController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IPasswordHasher<IdentityUser> _passwordHasher;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public PanelController(AppDbContext context, IPasswordHasher<IdentityUser> passwordHasher, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._context = context;
            this._passwordHasher = passwordHasher;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {

            //IdentityUser applicationUser = new IdentityUser();
            //Guid guid = Guid.NewGuid();
            //applicationUser.Id = guid.ToString();
            //applicationUser.UserName = "mertocak@gmail.com";
            //applicationUser.Email = "mertocak@gmail.com";
            //applicationUser.NormalizedUserName = "MERTOCAK@GMAIL.COM";

            //_context.Users.Add(applicationUser);


            //var hasedPassword = _passwordHasher.HashPassword(applicationUser, "123");
            //applicationUser.SecurityStamp = Guid.NewGuid().ToString();
            //applicationUser.PasswordHash = hasedPassword;

            //_context.SaveChanges();

            //var project = new Project
            //{
            //    Title = "Test Project",
            //    Description = "Description"

            //};
            //var category = new Category { CategoryName = "Future Technologies" };
            //project.ProjectCategories = new List<ProjectCategory>{
            //    new ProjectCategory {
            //            Project = project,
            //            Category = category,
            //           }
            //    };

            //_context.Projects.Add(project);
            //_context.SaveChanges();


            if (_signInManager.IsSignedIn(User))
            {
                //return View();
                return RedirectToAction("index", "project");
            }
            else
            {
                return View("login");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginViewModel model, string AccessDenied)
        {

            //model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                //if (user != null && !user.EmailConfirmed && (await _userManager.CheckPasswordAsync(user, model.Password)))
                //{
                //    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                //    return View(model);
                //}

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    //Prevent Open Redirect Attacks
                    if (!string.IsNullOrEmpty(AccessDenied) && Url.IsLocalUrl(AccessDenied))
                    {
                        return Redirect(AccessDenied);
                        //return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index");
                    }
                }

                if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }

            return View("login", model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index");
        }
    }
}