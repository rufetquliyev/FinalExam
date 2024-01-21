using AutoMapper;
using FinalExam.Business.Exceptions.Common;
using FinalExam.Business.Exceptions.User;
using FinalExam.Business.Services.Interfaces;
using FinalExam.Business.ViewModels.UserVMs;
using FinalExam.Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IAccountService service, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            _service = service;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserVm vm)
        {
            RegisterUserValidator validationRules = new RegisterUserValidator();
            var result = await validationRules.ValidateAsync(vm);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(vm);
            }
            if(!ModelState.IsValid) { return View(vm); }
            var res = await _service.Register(vm);
            if (!res.Succeeded)
            {
                foreach(var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserVm vm)
        {
            try
            {
                LoginUserValidator validationRules = new LoginUserValidator();
                var result = await validationRules.ValidateAsync(vm);
                if (!result.IsValid)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return View(vm);
                }
                if (!ModelState.IsValid) { return View(vm); }
                var user = await _service.Login(vm);
                var res = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
                return RedirectToAction("Index", "Home");
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return View(vm);
            }
        }
        public async Task<IActionResult> CreateRole()
        {
            await _service.CreateRole();
            return RedirectToAction("Index", "Home");
        } 
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
