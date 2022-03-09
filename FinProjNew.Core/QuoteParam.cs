using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinProjNew.Core
{
    public class QuoteParam
    {
        [Required]
        public string Ticker { get; set; }
        [Required]
        public string TimeFrame { get; set; } 
        public DateTimeOffset StartPeriod { get; set; } = DateTime.Today.AddDays(-5);
        public DateTimeOffset EndPeriod { get; set; } = DateTime.Today;
    }
}
