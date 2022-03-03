using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using FinProjNew.Core;

namespace FinProjNew.Data
{
    public class SQLData : IReadWrite
    {
        private readonly ApplicationContext _context;

        public SQLData(ApplicationContext context)
        {
            _context = context;
        }

        public List<Ticker> ReadTickers()
        {
            
            if((_context.Tickers.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            { 
                return _context.Tickers.ToList();
            }
            else
            {
                WriteTickers();
                return Parse.GetTickers("https://finance.yahoo.com/commodities");
            }
            
        } 

        public void WriteTickers()
        {
            
                _context.Database.EnsureCreated();

                List<Ticker> tickers = Parse.GetTickers("https://finance.yahoo.com/commodities");
                foreach (var ticker in tickers)
                {                    
                    _context.Tickers.Add(ticker);                    
                }
                _context.SaveChanges();
             
        }           
    }
}