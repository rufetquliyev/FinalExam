using FinalExam.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam.MVC.ViewComponents
{
    public class PortfolioViewComponent : ViewComponent
    {
        private readonly IPortfolioService _service;

        public PortfolioViewComponent(IPortfolioService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var portfolios = await _service.GetAllAsync();
            return View(portfolios.Take(6));
        }
    }
}
