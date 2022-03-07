using FinProjNew.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinProjNew.Data
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext() : base()
        {

        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Ticker> Tickers { get; set; }
        
    }
}
