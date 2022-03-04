﻿using FinProjNew.Core;
using FinProjNew.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinProjNew.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public QuoteParam Param { get; set; }
        [BindProperty]
        public List<Ticker> TickersList { get; set; }
        [BindProperty]
        public List<Quote> QuotesList { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            TickersList = _context.Tickers.ToList();
            QuotesList = CreateRequest(Param);

            return Page();
        }

        public List<Quote> CreateRequest(QuoteParam param)
        {
            var url = $"https://finance.yahoo.com/quote/{param.Ticker}/history?period1={(param.StartPeriod.AddDays(1)).ToUnixTimeSeconds()}&period2={param.EndPeriod.ToUnixTimeSeconds()}&interval={param.Period}&filter=history&frequency=1d&includeAdjustedClose=true";

            var quotes = new List<double>();
            var datesHeaders = Parse.GetQuotesHeaders(ref quotes, url);
            List<Quote> Quotes = Parse.ParseQuotes(datesHeaders, quotes);

            return Quotes;
        }
    }
}
