﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
