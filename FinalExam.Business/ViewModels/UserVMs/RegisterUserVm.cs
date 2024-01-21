using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalExam.Business.ViewModels.UserVMs
{
    public record RegisterUserVm
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class RegisterUserValidator : AbstractValidator<RegisterUserVm>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Fields cannot be empty.")
                .MaximumLength(30).WithMessage("Length cannot be more than 30 characters.");
            RuleFor(x => x.Surname).NotNull().NotEmpty().WithMessage("Fields cannot be empty.")
                .MaximumLength(50).WithMessage("Length cannot be more than 50 characters.");
            RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("Fields cannot be empty.")
                .MaximumLength(80).WithMessage("Length cannot be more than 80 characters.");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Fields cannot be empty.")
                .EmailAddress().WithMessage("Email is not in correct format.");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Fields cannot be empty.");
            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().WithMessage("Fields cannot be empty.")
                .Equal(x => x.Password).WithMessage("Confirm password must be the same with password.");
        }
    }
}
