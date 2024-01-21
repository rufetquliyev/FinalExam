using AutoMapper;
using FinalExam.Business.Exceptions.Common;
using FinalExam.Business.Exceptions.Portfolio;
using FinalExam.Business.Services.Interfaces;
using FinalExam.Business.ViewModels.PortfolioVMs;
using FinalExam.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _service;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public PortfolioController(IPortfolioService service, IMapper mapper, IWebHostEnvironment env)
        {
            _service = service;
            _mapper = mapper;
            _env = env;
        }
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Index()
        {
            var portfolios = await _service.GetAllAsync();
            return View(portfolios);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Create(CreatePortfolioVm vm)
        {
            try
            {
                CreatePortfolioValidator validationRules = new CreatePortfolioValidator();
                var result = await validationRules.ValidateAsync(vm);
                if (!result.IsValid)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    return View(vm);
                }
                if (!ModelState.IsValid) { return View(vm); }
                await _service.CreateAsync(vm, _env.WebRootPath);
                return RedirectToAction("Index");
            }
            catch (PortfolioImageException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return View(vm);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                Portfolio portfolio = await _service.GetByIdAsync(id);
                UpdatePortfolioVm vm = _mapper.Map<UpdatePortfolioVm>(portfolio);
                return View(vm);
            }
            catch (NegativeIdException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Index");
            }
            catch (PortfolioNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Update(UpdatePortfolioVm vm)
        {
            try
            {
                UpdatePortfolioValidator validationRules = new UpdatePortfolioValidator();
                var result = await validationRules.ValidateAsync(vm);
                if (!result.IsValid)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    return View(vm);
                }
                if (!ModelState.IsValid) { return View(vm); }
                await _service.UpdateAsync(vm, _env.WebRootPath);
                return RedirectToAction("Index");
            }
            catch (PortfolioImageException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return View(vm);
            }
            catch (NegativeIdException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Update");
            }
            catch (PortfolioNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Update");
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (NegativeIdException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Index");
            }
            catch (PortfolioNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}

