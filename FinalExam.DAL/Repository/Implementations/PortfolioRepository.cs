using FinalExam.Core.Entities;
using FinalExam.DAL.Context;
using FinalExam.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.DAL.Repository.Implementations
{
    public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(AppDbContext context) : base(context)
        {
        }
    }
}
