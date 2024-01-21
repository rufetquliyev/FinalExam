using FinalExam.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalExam.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPortfolioService _service;

        public HomeController(IPortfolioService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var portfolios = await _service.GetAllAsync();
            return View(portfolios);
        }
        public async Task<IActionResult> AccessDeniedCustom()
        {
            return View();
        }
    }
}
