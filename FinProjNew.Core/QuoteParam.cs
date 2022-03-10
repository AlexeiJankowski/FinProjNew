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
        [Display(Name = "Timeframe")]
        public string TimeFrame { get; set; } 
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset StartPeriod { get; set; } = DateTime.Today.AddDays(-20);
        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset EndPeriod { get; set; } = DateTime.Today;
    }
}
