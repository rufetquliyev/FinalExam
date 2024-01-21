using AutoMapper;
using FinalExam.Business.ViewModels.PortfolioVMs;
using FinalExam.Business.ViewModels.UserVMs;
using FinalExam.Core.Entities;

namespace FinalExam.MVC
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreatePortfolioVm, Portfolio>().ReverseMap();
            CreateMap<UpdatePortfolioVm, Portfolio>().ReverseMap();
            CreateMap<RegisterUserVm, AppUser>().ReverseMap();
            CreateMap<LoginUserVm, AppUser>().ReverseMap();
        }
    }
}
