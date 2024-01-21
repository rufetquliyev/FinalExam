using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Business.ViewModels.PortfolioVMs
{
    public record CreatePortfolioVm
    {
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
    }
    public class CreatePortfolioValidator : AbstractValidator<CreatePortfolioVm>
    {
        public CreatePortfolioValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Title cannot be empty.")
                .MaximumLength(30).WithMessage("Length cannot be more than 30 characters.");
            RuleFor(x => x.Image).NotNull().NotEmpty().WithMessage("Image file cannot be empty.");
        }
    }
}
