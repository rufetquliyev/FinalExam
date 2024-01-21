using FinalExam.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam.Core.Entities
{
    public class Portfolio : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string? ImgUrl { get; set; }
    }
}
