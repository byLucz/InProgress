﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XKANBAN.DTOs.Projects;
using XKANBAN.Models;
using XKANBAN.Models.Project;
using XKANBAN.Services.Interfaces;

namespace XKANBAN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IProjectService _projectService;

        public HomeController(ILogger<HomeController> logger, IProjectService projectService)
        {
            _logger = logger;
            _projectService = projectService;
        }

        public IActionResult Index(int pageId, string search)
        {
            //return View(_projectService.GetProjects());
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
