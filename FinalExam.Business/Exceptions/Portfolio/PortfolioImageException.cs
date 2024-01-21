using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Business.Exceptions.Portfolio
{
    public class PortfolioImageException : Exception
    {
        public string ParamName { get; set; }
        public PortfolioImageException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}
