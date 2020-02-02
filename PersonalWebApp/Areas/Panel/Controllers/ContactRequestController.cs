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
    public class ContactRequestController : Controller
    {
        private readonly IGenericRepository<ContactRequest> contactRequestRepository;

        public ContactRequestController(IGenericRepository<ContactRequest> contactRequestRepository)
        {
            this.contactRequestRepository = contactRequestRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(contactRequestRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            ContactRequest model = contactRequestRepository.GetById(id);
            return View("ContactRequestEditView", model);
        }
        [HttpPost]
        public IActionResult Edit(ContactRequest model)
        {
            if (ModelState.IsValid)
            {
                contactRequestRepository.Update(model);
                contactRequestRepository.Save();
                return RedirectToAction("Index");
            }
            return View("ContactRequestEditView", model);
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            contactRequestRepository.Delete(id);
            contactRequestRepository.Save();
            return RedirectToAction("Index");
        }
    }
}