﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;
using PersonalWebApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.ViewComponents
{
    public class VCardViewComponent : ViewComponent
    {
        private readonly IGenericRepository<General> generalRepository;

        public VCardViewComponent(IGenericRepository<General> generalRepository)
        {
            this.generalRepository = generalRepository;
        }

        public IViewComponentResult Invoke()
        {
            return View(generalRepository.GetById(Guid.Parse("fa02f728-016e-44cb-83d8-9dd157d5848b")));
        }
    }

}
