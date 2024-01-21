using FinalExam.Business.ViewModels.UserVMs;
using FinalExam.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Business.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<IdentityResult> Register(RegisterUserVm vm);
        public Task<AppUser> Login(LoginUserVm vm);
        public Task CreateRole();
    }
}
