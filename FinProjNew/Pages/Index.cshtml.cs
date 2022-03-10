using FinProjNew.Core;
using FinProjNew.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
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
        public QuoteParam Param { get; set; } = new QuoteParam();
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> TickersList { get; set; }
        [BindProperty]
        public List<Quote> QuotesList { get; set; }
        [BindProperty]
        public List<SelectListItem> TimeframeList { get; set; } = new List<SelectListItem>();

        public void OnGet()
        {
            ViewData["Title"] = "Home Page";
            LoadMenu();            
        }

        public IActionResult OnPost()
        {
            LoadMenu();
            if (string.IsNullOrEmpty(Param.Ticker) || string.IsNullOrEmpty(Param.TimeFrame))
            {
                ViewData["Title"] = "Home Page";
                return Page();
            }
            else
            {
                QuotesList = CreateRequest(Param);
                ViewData["Title"] = _context.Tickers.FirstOrDefault(x => x.TickerValue == Param.Ticker).TickerName;
                return Page();
            }            
        }

        public List<Quote> CreateRequest(QuoteParam param)
        {            
            var url = $"https://finance.yahoo.com/quote/{param.Ticker}/history?period1={(param.StartPeriod.AddDays(1)).ToUnixTimeSeconds()}&period2={param.EndPeriod.ToUnixTimeSeconds()}&interval={param.TimeFrame}&filter=history&frequency=1d&includeAdjustedClose=true";

            var quotes = new List<double>();
            var datesHeaders = Parse.GetQuotesHeaders(ref quotes, url);
            List<Quote> Quotes = Parse.ParseQuotes(datesHeaders, quotes);

            return Quotes;
        }

        public void LoadMenu()
        {
            if (!_context.Tickers.Any())
            {
                List<Ticker> tickers = Parse.GetTickers("https://finance.yahoo.com/commodities");
                foreach (var ticker in tickers)
                {
                    _context.Tickers.Add(ticker);
                }
                _context.SaveChanges();
            }

            TickersList = _context.Tickers.Select(x => new SelectListItem { Text = x.TickerName, Value = x.TickerValue }).ToList();
            TickersList.Insert(0, new SelectListItem { Text = "Select a ticker", Value = "" });

            Timeframe timeframe = new Timeframe();
            for (int i = 0; i < timeframe.TimeframeName.Count(); i++)
            {
                TimeframeList.Add(new SelectListItem { Text = timeframe.TimeframeName[i], Value = timeframe.TimeframeValue[i] });
            }

            TimeframeList.Insert(0, new SelectListItem { Text = "Select a timeframe", Value = "" });
        }
    }
}
