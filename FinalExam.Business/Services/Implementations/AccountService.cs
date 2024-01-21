using AutoMapper;
using FinalExam.Business.Enums;
using FinalExam.Business.Exceptions.Common;
using FinalExam.Business.Exceptions.User;
using FinalExam.Business.Services.Interfaces;
using FinalExam.Business.ViewModels.UserVMs;
using FinalExam.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if(!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString()
                    });
                }
            }
        }

        public async Task<AppUser> Login(LoginUserVm vm)
        {
            var user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail) ?? await _userManager.FindByNameAsync(vm.UsernameOrEmail);
            var res = await _userManager.CheckPasswordAsync(user, vm.Password);
            if(!res) throw new UserNotFoundException("Username / Email or password is wrong.", nameof(vm));
            if (user == null) throw new UserNotFoundException("Username / Email or password is wrong.", nameof(vm.UsernameOrEmail));
            return user;
        }

        public async Task<IdentityResult> Register(RegisterUserVm vm)
        {
            AppUser user = _mapper.Map<AppUser>(vm);
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
            }
            return result;
        }
    }
}
