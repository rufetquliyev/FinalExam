using FinalExam.Business.ViewModels.PortfolioVMs;
using FinalExam.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Business.Services.Interfaces
{
    public interface IPortfolioService
    {
        public Task<IQueryable<Portfolio>> GetAllAsync();
        public Task<Portfolio> GetByIdAsync(int id);
        public Task CreateAsync(CreatePortfolioVm vm, string env);
        public Task UpdateAsync(UpdatePortfolioVm vm, string env);
        public Task DeleteAsync(int id);
    }
}
