using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using API.Dtos;
using API.Helpers;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OptionTracker.Data;
using OptionTracker.Models;
using OptionTracker.Services;

namespace OptionTracker.Controllers
{
    public class TickersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TickersController> _logger;
        private readonly IApiService _apiService;

        public TickersController(ApplicationDbContext context, IApiService apiService,
            ILogger<TickersController> logger)
        {
            _context = context;
            _apiService = apiService;
            _logger = logger;
        }

        // GET: Ticker
        public async Task<IActionResult> Index([FromQuery] TickerSpecParams productParams)
        {
            var client = new HttpClient();

            productParams.PageSize = 50;

            var longurl = "https://core-api-qepiuuzgya-uc.a.run.app/api/Products";

            var uriBuilder = new UriBuilder(longurl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);


            query["PageIndex"] = productParams.PageIndex.ToString();
            query["PageSize"] = productParams.PageSize.ToString();
            query["BrandId"] = productParams.BrandId.ToString();
            query["TypeId"] = productParams.TypeId.ToString();


            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                query["Sort"] = productParams.Sort;
                ViewData["Sort"] = productParams.Sort;
            }

            if (!string.IsNullOrEmpty(productParams.Search)) query["Search"] = productParams.Search;


            uriBuilder.Query = query.ToString();
            longurl = uriBuilder.ToString();
            // Products?Sort=capDesc
            // Products?PageSize=50&PageIndex=1&Sort=capDesc
            // Products?PageIndex=2&PageSize=2&Sort=capDesc
            var answer = await client.GetFromJsonAsync<JsonDocument>(longurl);

            var tickers = JsonConvert
                .DeserializeObject<TickerToReturnDto[]>(answer.RootElement.GetProperty("data").ToString()).ToList();

            var count = answer.RootElement.GetProperty("count").GetInt32();


            return View(new Pagination<TickerToReturnDto>(productParams.PageIndex,
                productParams.PageSize, count, tickers));
        }

        [HttpPost("Tickers/GetOptionChartData/{ticker}")]
        public JsonResult GetOptionChartData(string ticker)
        {
            try
            {
                var sp = ticker.Split("_");
                var volumeAnal = _context.OptionChainRaw
                    .Where(m => m.Symbol == sp[0]).ToList();

                var dates = volumeAnal.Select(x => JsonConvert
                    .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                        x.Data.RootElement.GetProperty("callExpDateMap").ToString()));

                var opData = dates
                    .SelectMany(x => x.Values)
                    .SelectMany(x => x.Values)
                    .SelectMany(x => x)
                    .Where(x => x.Symbol == ticker)
                    .OrderByDescending(x => x.QuoteTimeInLong)
                    .Take(30)
                    .ToList();

                var graph1 = opData.Select(x => new
                    {x = x.QuoteTimeInLong, y = new[] {x.Last, x.ClosePrice, x.LowPrice, x.HighPrice}}).ToArray();
                var SeriesVal = graph1;

                var graph2 = opData.Select(x => new {x = x.QuoteTimeInLong, y = new[] {x.TotalVolume}}).ToArray();

                var LabelsVal = graph2;
                return Json(new {success = true, series = SeriesVal, labels = LabelsVal, message = "success.!"});
            }
            catch (Exception ex)
            {
                return Json(new {success = false, message = "Some thing went Wrong.! unsuccessfull!"});
            }
        }

        // GET: Ticker/Details/5
        // [ResponseCache(VaryByHeader = "", Location = ResponseCacheLocation.Client, Duration = 30)]
        [HttpGet("Tickers/Details/{symbol}")]
        public async Task<IActionResult> Details(string symbol, [FromQuery] TickerSpecParams productParams)
        {
            if (symbol == null) return NotFound();
            var ticker = await _context.TickerSymbols
                .FirstOrDefaultAsync(m => m.Name.Equals(symbol.ToUpper()));

            if (ticker == null) return NotFound();

            var chainRaw = new OptionChainRaw();

            if (productParams.BrandId != null)
            {
                ViewData["Brand"] = productParams.BrandId;

                chainRaw = await _context.OptionChainRaw.Where(x =>
                        x.Symbol == ticker.Name)
                    .OrderByDescending(x => x.Id).FirstOrDefaultAsync();

                var day = chainRaw.Created.AddDays(-productParams.BrandId.GetValueOrDefault());

                chainRaw = await _context.OptionChainRaw.Where(x =>
                    x.Symbol == ticker.Name && x.Created.Date == day).FirstOrDefaultAsync();
            }
            else
            {
                ViewData["Brand"] = "0";
                chainRaw = await _context.OptionChainRaw.Where(x =>
                        x.Symbol == ticker.Name)
                    .OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            }

            OptionContract[] s;


            if (productParams.TypeId == 1)
                s = JsonConvert
                    .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                        chainRaw.Data.RootElement.GetProperty("putExpDateMap").ToString() ?? "")
                    .SelectMany(a => a.Value.Values)
                    .SelectMany(x => x).ToArray();
            else
                s = JsonConvert
                    .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                        chainRaw.Data.RootElement.GetProperty("callExpDateMap").ToString() ?? "")
                    .SelectMany(a => a.Value.Values)
                    .SelectMany(x => x).ToArray();

            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var dtDateTime = dt.AddMilliseconds(s[0].QuoteTimeInLong.GetValueOrDefault()).ToLocalTime();

            var viewModel = new ChainResultViewModel
            {
                Ticker = ticker.Name,
                MarketCap = ticker.MarketCap,
                ClosePrice = ticker.ClosePrice,
                Created = dtDateTime,

                OptionsResults = s
                    .Select(both => new OptionResultViewModel
                    {
                        Description = both.Description,
                        Symbol = both.Symbol,
                        ChartCode = CreateChartCode(both.Description),
                        OpenInterest = both.OpenInterest,
                        Volume = both.TotalVolume,
                        ClosePrice = both.Last,
                        OpenInterestChange = both.OpenInterest - both.OpenInterest,
                        ClosePriceChange = both.Last - both.Last
                    })
                    .ToList()
            };

            if (!string.IsNullOrEmpty(productParams.Sort)) ViewData["Sort"] = productParams.Sort;
            if (productParams.TypeId != null) ViewData["Type"] = productParams.TypeId;


            productParams.PageSize = 50;

            if (string.IsNullOrEmpty(productParams.Sort) || productParams.Sort == "oiDesc")
                viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.OpenInterest).ToList();

            if (productParams.Sort == "valueDesc")
                viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.TotalValue).ToList();


            if (productParams.Sort == "oicDesc")
                viewModel.OptionsResults =
                    viewModel.OptionsResults.OrderByDescending(x => x.OpenInterestChange).ToList();

            if (productParams.Sort == "cpcDesc")
                viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.ClosePriceChange).ToList();

            if (productParams.Sort == "cpDesc")
                viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.ClosePrice).ToList();

            if (productParams.Sort == "volumeDesc")
                viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.Volume).ToList();

            if (productParams.Search != null &&
                (productParams.Search.Equals("dWeek") || productParams.Search.Equals("threeD")))
            {
                var chainRaws = await _context.OptionChainRaw.Where(x =>
                        x.Data.RootElement.GetProperty("symbol").GetString() == ticker.Name)
                    .OrderByDescending(x => x.Id).Take(7).ToListAsync();

                viewModel = CreateChainResultViewModel(chainRaws, productParams.Search, ticker);
            }

            var c = viewModel.OptionsResults.Count;

            viewModel.OptionsResults =
                viewModel.OptionsResults.Take(productParams.PageSize).ToList();

            return View(new Pagination<ChainResultViewModel>(productParams.PageIndex,
                productParams.PageSize, c, new List<ChainResultViewModel> {viewModel}));
        }

        [HttpPost("Tickers/GetChartData/{ticker}")]
        public JsonResult GetChartData(string ticker)
        {
            try
            {
                var chainRaw = _context.OptionChainRaw.Where(x =>
                        x.Data.RootElement.GetProperty("symbol").GetString() == ticker)
                    .OrderByDescending(x => x.Id).FirstOrDefault();

                var s = JsonConvert
                    .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                        chainRaw.Data.RootElement.GetProperty("callExpDateMap").ToString() ?? "")
                    .SelectMany(a => a.Value.Values)
                    .SelectMany(x => x).OrderByDescending(x => x.OpenInterest).ToArray();
                var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                var dtDateTime = dt.AddMilliseconds(s[0].QuoteTimeInLong.GetValueOrDefault()).ToLocalTime();

                var ot = s.Select(x => x.OpenInterest).Skip(5).Sum();
                decimal[] SeriesVal =
                    {s[0].OpenInterest, s[1].OpenInterest, s[2].OpenInterest, s[3].OpenInterest, s[4].OpenInterest, ot};
                string[] LabelsVal =
                {
                    s[0].Description, s[1].Description, s[2].Description, s[3].Description, s[4].Description, "Others"
                };
                return Json(new {success = true, series = SeriesVal, labels = LabelsVal, message = "success.!"});
            }
            catch (Exception ex)
            {
                return Json(new {success = false, message = "Some thing went Wrong.! unsuccessfull!"});
            }
        }

        [HttpPost("Tickers/GetChart/{desc}")]
        public async Task<JsonResult> GetChart(string desc)
        {
            try
            {
                var sy = desc.Split("_")[0];


                var chainRaws = await _context.OptionChainRaw.Where(x =>
                    x.Data.RootElement.GetProperty("symbol").GetString() == sy).ToListAsync();

                var s = chainRaws.Select(chainRaw => JsonConvert
                    .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                        chainRaw.Data.RootElement.GetProperty("callExpDateMap").ToString() ?? "")
                    .SelectMany(a => a.Value.Values)
                    .SelectMany(x => x));

                var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                var dtDateTime =
                    dt.AddMilliseconds(s.FirstOrDefault().FirstOrDefault().QuoteTimeInLong.GetValueOrDefault())
                        .ToLocalTime();


                var p = s.Where(x => x.Any(c => c.Symbol.Equals(desc))).SelectMany(a => a);

                var op = p.First();
                return Json(new {success = true, series = p.ToString(), labels = "", message = "success.!"});
            }
            catch (Exception ex)
            {
                return Json(new {success = false, message = "Some thing went Wrong.! unsuccessfull!"});
            }
        }

        private ChainResultViewModel CreateChainResultViewModel(IList<OptionChainRaw> raws, string id,
            TickerSymbol tickerSymbol)
        {
            var oldChainRaw = new OptionChainRaw();

            var chainRaw = raws.FirstOrDefault();
            if (id != null && id.Equals("dWeek"))
                oldChainRaw = raws.Last();

            else if (id != null && id.Equals("threeD"))
                oldChainRaw = raws.Skip(2).Take(1).FirstOrDefault();

            var viewModel = new ChainResultViewModel();
            var listOp = new List<OptionResultViewModel>();

            var os = JsonConvert
                .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                    chainRaw.Data.RootElement.GetProperty("callExpDateMap").ToString() ?? "")
                .SelectMany(a => a.Value.Values)
                .SelectMany(x => x).ToArray();

            if (oldChainRaw == null)
                viewModel = new ChainResultViewModel
                {
                    Ticker = chainRaw.Data.RootElement.GetProperty("symbol").GetString(),
                    MarketCap = tickerSymbol.MarketCap,
                    ClosePrice = tickerSymbol.ClosePrice,
                    Created = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        .AddMilliseconds(os[0].QuoteTimeInLong.GetValueOrDefault()).ToLocalTime(),

                    OptionsResults = os
                        .Select(both => new OptionResultViewModel
                        {
                            Description = both.Description,
                            Symbol = both.Symbol,
                            ChartCode = CreateChartCode(both.Description),
                            OpenInterest = both.OpenInterest,
                            ClosePrice = both.Last,
                            OpenInterestChange = both.OpenInterest - both.OpenInterest,
                            ClosePriceChange = both.Last - both.Last
                        })
                        .ToList()
                };
            if (oldChainRaw != null)
            {
                foreach (var a in JsonConvert
                    .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                        chainRaw.Data.RootElement.GetProperty("callExpDateMap").ToString() ?? "")
                    .SelectMany(a => a.Value.Values)
                    .SelectMany(x => x).ToArray())
                {
                    var b = JsonConvert
                        .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                            oldChainRaw.Data.RootElement.GetProperty("callExpDateMap").ToString() ?? "")
                        .SelectMany(a => a.Value.Values)
                        .SelectMany(x => x).ToArray().SingleOrDefault(x => x.Symbol == a.Symbol);

                    if (b != null)
                    {
                        var s = new OptionResultViewModel
                        {
                            Description = a.Description,
                            Symbol = a.Symbol,
                            OpenInterest = a.OpenInterest,
                            ChartCode = CreateChartCode(a.Description),
                            ClosePrice = a.Last,
                            OpenInterestChange = b.OpenInterest - a.OpenInterest,
                            ClosePriceChange = b.Last - a.Last
                        };
                        listOp.Add(s);
                    }
                }


                viewModel = new ChainResultViewModel
                {
                    Ticker = chainRaw.Data.RootElement.GetProperty("symbol").GetString(),
                    Created = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        .AddMilliseconds(os[0].QuoteTimeInLong.GetValueOrDefault()).ToLocalTime(),
                    MarketCap = tickerSymbol.MarketCap,
                    ClosePrice = tickerSymbol.ClosePrice,
                    TimeChange = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                                     .AddMilliseconds(os[0].QuoteTimeInLong.GetValueOrDefault()).ToLocalTime() -
                                 new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                                     .AddMilliseconds(os[0].QuoteTimeInLong.GetValueOrDefault()).ToLocalTime(),

                    OptionsResults = listOp
                };
            }

            viewModel.OptionsResults =
                viewModel.OptionsResults.OrderByDescending(x => x.OpenInterest).Take(50).ToList();
            return viewModel;
        }

        private string CreateChartCode(string bothDescription)
        {
            //.MA210618C350
            var all = bothDescription.Split(" ");
            // MA Mar 19 2021 300 Put
            var sb = new StringBuilder();
            DateTime parsedDate;
            sb.Append(".");
            sb.Append(all[0]);

            var dt = DateTime.TryParseExact(all[3] + all[1] + all[2], "yyyyMMMdd", null,
                DateTimeStyles.None, out parsedDate);
            sb.Append(parsedDate.ToString("yyMMdd"));
            var cp = all[5].Equals("Call") ? "C" : "P";
            sb.Append(cp);
            sb.Append(all[4]);
            return sb.ToString();
        }

        // GET: Ticker/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ticker/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TickerToReturnDto ticker)
        {
            if (ModelState.IsValid)
            {
                if (!_context.TickerSymbols.Any(x => x.Name == ticker.Name))
                {
                }

                return RedirectToAction(nameof(Index));
            }

            return View(ticker);
        }

        // GET: Ticker/Edit/5
        [HttpGet("Tickers/Edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null) return NotFound();

            var sy = id.Split("_")[0];


            var chainRaw = await _context.OptionChainRaw.Where(x =>
                    x.Data.RootElement.GetProperty("symbol").GetString().Equals(sy))
                .OrderByDescending(x => x.Id).FirstOrDefaultAsync();


            var s = JsonConvert
                .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                    chainRaw.Data.RootElement.GetProperty("callExpDateMap").ToString() ?? "")
                .SelectMany(a => a.Value.Values)
                .SelectMany(x => x).ToArray();

            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var dtDateTime = dt.AddMilliseconds(s[0].QuoteTimeInLong.GetValueOrDefault()).ToLocalTime();


            var p = s.Where(x => x.Symbol.Equals(id));

            var op = p.First();
            if (op == null) return NotFound();
            return View(op);
        }

        // POST: Ticker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TickerToReturnDto ticker)
        {
            if (id != ticker.Id) return NotFound();

            //if (ModelState.IsValid)
            //{
            //    //try
            //    //{
            //    //    var defaultWatchlist = await _context.Watchlist.FirstOrDefaultAsync();
            //    //    if (defaultWatchlist == null)
            //    //    {
            //    //        var watchlist = new Watchlist { TickerList = new List<string>() };
            //    //        await _context.Watchlist.AddAsync(watchlist);
            //    //        await _context.SaveChangesAsync();
            //    //    }

            //    //    var updatedList = await _context.Watchlist.FirstOrDefaultAsync();

            //    //    updatedList.TickerList.Add(ticker.Name);

            //    //    await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!TickerExists(ticker.Id))
            //            return NotFound();
            //        throw;
            //    }

            //    return RedirectToAction(nameof(Index));
            //}

            return View(ticker);
        }

        // GET: Ticker/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ticker = await _context.TickerSymbols
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticker == null) return NotFound();

            return View(ticker);
        }

        // POST: Ticker/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var ticker = await _context.Ticker.FindAsync(id);

            //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var trader = await _context.Traders.FirstOrDefaultAsync(x => x.IdentityUserId == userId);
            //if (trader == null)
            //{
            //    var newTrader = new Trader() { IdentityUserId = userId };
            //    await _context.Traders.AddAsync(newTrader);
            //   // ticker.Traders.Add(newTrader);
            //    await _context.SaveChangesAsync();
            //}
            //else
            //{
            //   // ticker.Traders.Add(trader);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToAction(nameof(Index));
        }

        private bool TickerExists(int id)
        {
            return _context.TickerSymbols.Any(e => e.Id == id);
        }
    }
}