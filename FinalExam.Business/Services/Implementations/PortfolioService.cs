using AutoMapper;
using FinalExam.Business.Exceptions.Common;
using FinalExam.Business.Exceptions.Portfolio;
using FinalExam.Business.Helpers;
using FinalExam.Business.Services.Interfaces;
using FinalExam.Business.ViewModels.PortfolioVMs;
using FinalExam.Core.Entities;
using FinalExam.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Business.Services.Implementations
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _repository;
        private readonly IMapper _mapper;

        public PortfolioService(IPortfolioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IQueryable<Portfolio>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Portfolio> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException("Invalid ID received.", nameof(id));
            Portfolio portfolio = await _repository.GetByIdAsync(id);
            if (portfolio == null) throw new PortfolioNotFoundException("The portfolio not found.", nameof(portfolio));
            return portfolio;
        }
        public async Task CreateAsync(CreatePortfolioVm vm, string env)
        {
            if (!vm.Image.CheckImg()) throw new PortfolioImageException("File format must be image and size must be lower than 3MB", nameof(vm.Image));
            Portfolio portfolio = _mapper.Map<Portfolio>(vm);
            portfolio.ImgUrl = vm.Image.UploadImg(env, @"/Upload/PortfolioImages/");
            await _repository.CreateAsync(portfolio);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Portfolio portfolio = await GetByIdAsync(id);
            await _repository.DeleteAsync(portfolio);
            await _repository.SaveChangesAsync();
        }


        public async Task UpdateAsync(UpdatePortfolioVm vm, string env)
        {
            Portfolio portfolio = await GetByIdAsync(vm.Id);
            _mapper.Map(vm, portfolio);
            if (vm.Image != null)
            {
                if (!vm.Image.CheckImg()) throw new PortfolioImageException("File format must be image and size must be lower than 3MB", nameof(vm.Image));
                portfolio.ImgUrl.DeleteImg(env, @"/Upload/PortfolioImages/");
                portfolio.ImgUrl = vm.Image.UploadImg(env, @"/Upload/PortfolioImages/");
            }
            await _repository.UpdateAsync(portfolio);
            await _repository.SaveChangesAsync();
        }
    }
}
