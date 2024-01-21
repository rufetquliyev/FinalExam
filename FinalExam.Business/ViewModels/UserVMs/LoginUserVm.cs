using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalExam.Business.ViewModels.UserVMs
{
    public record LoginUserVm
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
    public class LoginUserValidator : AbstractValidator<LoginUserVm>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotNull().NotEmpty().WithMessage("Username / Email or password cannot be empty.")
                .MaximumLength(80).WithMessage("Length cannot be more than 80 characters.");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Username / Email or password cannot be empty.");
        }
    }
}
