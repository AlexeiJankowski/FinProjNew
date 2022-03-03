using FinProjNew.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinProjNew.Data
{
    public interface IReadWrite
    {
        List<Ticker> ReadTickers();
        void WriteTickers();
    }
}
