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
        public string Ticker { get; set; } = "AAPL";
        public string Period { get; set; } = "d1";
        public DateTimeOffset StartPeriod { get; set; } = DateTime.Now.AddDays(-1);
        public DateTimeOffset EndPeriod { get; set; } = DateTime.Now;
    }
}
