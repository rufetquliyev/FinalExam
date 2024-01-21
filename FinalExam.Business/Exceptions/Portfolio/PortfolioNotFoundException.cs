using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Business.Exceptions.Portfolio
{
    public class PortfolioNotFoundException : Exception
    {
        public string ParamName { get; set; }
        public PortfolioNotFoundException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}
