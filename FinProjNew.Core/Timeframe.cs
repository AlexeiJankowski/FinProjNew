using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinProjNew.Core
{
    public class Timeframe
    {
        public List<string> TimeframeName { get; set; } = new List<string> { "Daily", "Weekly", "Monthly" };
        public List<string> TimeframeValue { get; set; } = new List<string> { "1d", "1wk", "1mo" };
    }
}
