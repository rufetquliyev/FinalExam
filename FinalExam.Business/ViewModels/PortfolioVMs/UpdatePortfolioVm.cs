using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Business.ViewModels.PortfolioVMs
{
    public record UpdatePortfolioVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public bool isDeleted { get; set; }
        public IFormFile? Image { get; set; }
    }
    public class UpdatePortfolioValidator : AbstractValidator<UpdatePortfolioVm>
    {
        public UpdatePortfolioValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Title cannot be empty.")
                .MaximumLength(30).WithMessage("Length cannot be more than 30 characters.");
        }
    }
}
