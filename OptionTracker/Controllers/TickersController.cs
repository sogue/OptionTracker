using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OptionTracker.Data;
using OptionTracker.Models;
using OptionTracker.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using API.Helpers;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using OptionTracker.Models.Anal;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

            string longurl = "https://core-api-qepiuuzgya-uc.a.run.app/api/Products";

            var uriBuilder = new UriBuilder(longurl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            query["PageSize"] = "50";
            query["PageIndex"] = productParams.PageIndex.ToString();

            if (!string.IsNullOrEmpty(productParams.Search))
            {
                query["Search"] = productParams.Search;
            }
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                query["Sort"] = productParams.Sort;
            }

            uriBuilder.Query = query.ToString();
            longurl = uriBuilder.ToString();

            var answer = await client.GetFromJsonAsync<JsonDocument>(longurl);

            var tickers = JsonConvert
                .DeserializeObject<Ticker[]>(answer.RootElement.GetProperty("data").ToString()).ToList();



            return View(new Pagination<Ticker>(productParams.PageIndex,
                productParams.PageSize, 100, tickers));
        }

        // GET: Ticker/Details/5
        [Microsoft.AspNetCore.Mvc.HttpGet("Tickers/TotalDetails/")]
        [Microsoft.AspNetCore.Mvc.Route("Tickers/TotalDetails/{id}")]
        public async Task<IActionResult> DetailsTotal(string? id)
        {
            var chainRaws = await _context.CompareRaw.ToListAsync();

            var result = chainRaws.OrderByDescending(x => x.OpenInterestChange).Take(50).ToList();

            if (id != null && id.Equals("true"))
                result = result.OrderByDescending(x => x.TotalValue).ToList();

            if (id != null && id.Equals("oChange"))
                result = result.OrderByDescending(x => x.OpenInterestChange).ToList();

            if (id != null && id.Equals("cChange"))
                result = result.OrderByDescending(x => x.ClosePriceChange).ToList();

            return View(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("Tickers/GetOptionChartData/{ticker}")]
        public JsonResult GetOptionChartData(string ticker)
        {
            try
            {
                var sp = ticker.Split("_");
               var volumeAnal = _context.OptionChainRaw
                   .Where(m => m.Symbol == sp[0]).ToList();
              
               var dates = volumeAnal.Select(x=> JsonConvert
                   .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                       x.Data.RootElement.GetProperty("callExpDateMap").ToString()));

               var opData = dates
                   .SelectMany(x=>x.Values)
                   .SelectMany(x=>x.Values)
                   .SelectMany(x=>x)
                   .Where(x=>x.Symbol == ticker)
                   .OrderByDescending(x=>x.QuoteTimeInLong)
                   .Take(30)
                   .ToList();

               var graph1 = opData.Select(x=>new {x=x.QuoteTimeInLong , y= new[]{ x.Last , x.ClosePrice , x.LowPrice , x.HighPrice } }).ToArray();
                var SeriesVal = graph1;

                var graph2 = opData.Select(x => new { x = x.QuoteTimeInLong, y = new[] { x.TotalVolume } }).ToArray();

                var LabelsVal = graph2;
                return Json(new { success = true, series = SeriesVal, labels = LabelsVal, message = "success.!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Some thing went Wrong.! unsuccessfull!" });
            }
        }

        // GET: Ticker/Details/5
        // [ResponseCache(VaryByHeader = "", Location = ResponseCacheLocation.Client, Duration = 30)]
        [Microsoft.AspNetCore.Mvc.HttpGet("Tickers/Details/{symbol}/{id}")]
        [Route("Tickers/Details/{symbol}",
            Name = "speakerevalscurrent")]
        public async Task<IActionResult> Details(string symbol, string? id, string? type)
        {
            if (symbol == null) return NotFound();
            var ticker = await _context.Ticker
                .FirstOrDefaultAsync(m => m.Symbol.Equals(symbol.ToUpper()));

            if (ticker == null) return NotFound();

            var chainRaw = new OptionChainRaw();

            if (id != null && id.Equals("update"))
            {
                var y = await _apiService.GetContractsByTickerName(ticker.Symbol);


                if (y != null)
                {
                    chainRaw = new OptionChainRaw
                    {
                        Data = y
                    };

                    //_logger.LogWarning("Log - Raw Save Start:" + DateTime.Now);
                    // await _context.OptionChainRaw.AddAsync(optionChainRaw);
                    // await _context.SaveChangesAsync();
                    // _logger.LogWarning("Log - Raw Save Done:" + DateTime.Now);
                }
            }
            else
            {
                try
                {
                    chainRaw = await _context.OptionChainRaw.Where(x =>
                            x.Data.RootElement.GetProperty("symbol").GetString() == ticker.Symbol)
                        .OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                }
                catch (Exception e)
                {
                    var y = await _apiService.GetContractsByTickerName(ticker.Symbol);


                    if (y != null)
                    {
                        var optionChainRaw = new OptionChainRaw
                        {
                            Data = y
                        };

                        _logger.LogWarning("Log - Raw Save Start:" + DateTime.Now);
                        await _context.OptionChainRaw.AddAsync(optionChainRaw);
                        await _context.SaveChangesAsync();
                        _logger.LogWarning("Log - Raw Save Done:" + DateTime.Now);

                        chainRaw = optionChainRaw;
                    }

                }
            }

            OptionContract[] s;

            if(type == "put")
            {
                s = JsonConvert
                    .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                        chainRaw.Data.RootElement.GetProperty("putExpDateMap").ToString() ?? "")
                    .SelectMany(a => a.Value.Values)
                    .SelectMany(x => x).ToArray();
            }
            else
            {
                s = JsonConvert
                    .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                        chainRaw.Data.RootElement.GetProperty("callExpDateMap").ToString() ?? "")
                    .SelectMany(a => a.Value.Values)
                    .SelectMany(x => x).ToArray();

               
            }
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            var dtDateTime = dt.AddMilliseconds(s[0].QuoteTimeInLong.GetValueOrDefault()).ToLocalTime();
            var viewModel = new ChainResultViewModel
            {
                Ticker = ticker.Symbol,
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

            // var viewModels = await _context.ComparedChains.Include(s=>s.OptionsResults).Where(x => x.Ticker.Equals(symbol)).ToListAsync();


            //  var viewModel = id != null && id.Equals("dWeek")
            //                ? viewModels.FirstOrDefault(x => x.TimeChange > new TimeSpan(6, 23, 0, 0, 0))
            //                : id != null && id.Equals("3d")
            //                    ? viewModels.FirstOrDefault(x =>
            //                        x.TimeChange > new TimeSpan(2, 23, 0, 0, 0) &&
            //                        x.TimeChange < new TimeSpan(6, 23, 0, 0, 0))
            //                    : viewModels.FirstOrDefault(x => x.TimeChange < new TimeSpan(1, 23, 0, 0, 0)) 
            //;

            //if (viewModel == null)
            //{
            //   // viewModel = await CreateChainResultViewModel(id, ticker);
            //}
            if (id == null || id == "update")
                viewModel.OptionsResults =
                viewModel.OptionsResults.OrderByDescending(x => x.Volume).Take(50).ToList();

            if (id != null && id.Equals("true"))
                viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.TotalValue).ToList();

            if (id != null && id.Equals("false"))
                viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.OpenInterest).ToList();

            if (id != null && id.Equals("oChange"))
                viewModel.OptionsResults =
                    viewModel.OptionsResults.OrderByDescending(x => x.OpenInterestChange).ToList();

            if (id != null && id.Equals("cChange"))
                viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.ClosePriceChange).ToList();

            if (id != null && id.Equals("volume"))
                viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.Volume).ToList();

            if (id != null && (id.Equals("dWeek") || id.Equals("threeD")))
            {
                var chainRaws = await _context.OptionChainRaw.Where(x =>
                        x.Data.RootElement.GetProperty("symbol").GetString() == ticker.Symbol)
                    .OrderByDescending(x => x.Id).Take(7).ToListAsync();

                viewModel = CreateChainResultViewModel(chainRaws,id,ticker);
                
            }

            viewModel.OptionsResults =
                viewModel.OptionsResults.Take(50).ToList();

            return View(viewModel);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("Tickers/GetChartData/{ticker}")]
        public JsonResult GetChartData(string ticker)
        {
            try
            {
                var chainRaw =  _context.OptionChainRaw.Where(x =>
                        x.Data.RootElement.GetProperty("symbol").GetString() == ticker)
                    .OrderByDescending(x => x.Id).FirstOrDefault();

                var s = JsonConvert
                    .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                        chainRaw.Data.RootElement.GetProperty("callExpDateMap").ToString() ?? "")
                    .SelectMany(a => a.Value.Values)
                    .SelectMany(x => x).OrderByDescending(x => x.OpenInterest).ToArray();
                var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                var dtDateTime = dt.AddMilliseconds(s[0].QuoteTimeInLong.GetValueOrDefault()).ToLocalTime();

                var ot = s.Select(x => x.OpenInterest).Skip(5).Sum();
                decimal[] SeriesVal = { s[0].OpenInterest, s[1].OpenInterest, s[2].OpenInterest, s[3].OpenInterest, s[4].OpenInterest, ot };
                string[] LabelsVal = { s[0].Description, s[1].Description, s[2].Description, s[3].Description, s[4].Description, "Others" };
                return Json(new { success = true, series = SeriesVal, labels = LabelsVal, message = "success.!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Some thing went Wrong.! unsuccessfull!" });
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("Tickers/GetChart/{desc}")]
        public async Task<JsonResult> GetChart(string desc)
        {
            try
            {
                var sy = desc.Split("_")[0];


                var chainRaws = await _context.OptionChainRaw.Where(x =>
                        x.Data.RootElement.GetProperty("symbol").GetString() == sy).ToListAsync();

               var s= chainRaws.Select(chainRaw => JsonConvert
                    .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                        chainRaw.Data.RootElement.GetProperty("callExpDateMap").ToString() ?? "")
                    .SelectMany(a => a.Value.Values)
                    .SelectMany(x => x));

               var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                var dtDateTime = dt.AddMilliseconds(s.FirstOrDefault().FirstOrDefault().QuoteTimeInLong.GetValueOrDefault()).ToLocalTime();


                var p = s.Where(x => x.Any(c=>c.Symbol.Equals(desc))).SelectMany(a=>a);

                var op = p.First();
                return Json(new { success = true, series = p.ToString(), labels = "", message = "success.!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Some thing went Wrong.! unsuccessfull!" });
            }
        }

        private  ChainResultViewModel CreateChainResultViewModel(IList<OptionChainRaw> raws, string id, Ticker ticker)
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
                    MarketCap = ticker.MarketCap,
                    ClosePrice = ticker.ClosePrice,
                    Created = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
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
                    Created = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
                        .AddMilliseconds(os[0].QuoteTimeInLong.GetValueOrDefault()).ToLocalTime(),
                    MarketCap = ticker.MarketCap,
                    ClosePrice = ticker.ClosePrice,
                    TimeChange = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
                        .AddMilliseconds(os[0].QuoteTimeInLong.GetValueOrDefault()).ToLocalTime() -
                    new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
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
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Microsoft.AspNetCore.Mvc.Bind("Id,Symbol")] Ticker ticker)
        {
            if (ModelState.IsValid)
            {
                if (!_context.Ticker.Any(x => x.Symbol == ticker.Symbol))
                {
                    _context.Add(ticker);
                    await _context.SaveChangesAsync();

                    var y = await _apiService.GetContractsByTickerName(ticker.Symbol);

                    var contracts =
                        new ChainRaw
                        {
                            Chain = new Chain
                            {
                                Symbol = y.RootElement.GetProperty("symbol").GetString(),
                                UnderlyingPrice = y.RootElement.GetProperty("underlyingPrice").GetDecimal(),
                                OptionContracts = JsonConvert
                                    .DeserializeObject<Dictionary<string, Dictionary<string, OptionContract[]>>>(
                                        y.RootElement.GetProperty("callExpDateMap").ToString() ?? "")
                                    .SelectMany(a => a.Value.Values)
                                    .SelectMany(x => x).ToArray()
                            }
                        };

                    _logger.LogWarning("Log - Poco Save Start:" + DateTime.Now);
                    await _context.ChainRaw.AddRangeAsync(contracts);
                    await _context.SaveChangesAsync();
                    _logger.LogWarning("Log - Poco Save Done:" + DateTime.Now);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(ticker);
        }

        // GET: Ticker/Edit/5
        [Microsoft.AspNetCore.Mvc.HttpGet("Tickers/Edit/{id}")]
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

            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            var dtDateTime = dt.AddMilliseconds(s[0].QuoteTimeInLong.GetValueOrDefault()).ToLocalTime();


            var p = s.Where(x => x.Symbol.Equals(id));

            var op = p.First();
            if (op == null) return NotFound();
            return View(op);
        }

        // POST: Ticker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Microsoft.AspNetCore.Mvc.Bind("Id,Symbol")] Ticker ticker)
        {
            if (id != ticker.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var defaultWatchlist = await _context.Watchlist.FirstOrDefaultAsync();
                    if (defaultWatchlist == null)
                    {
                        var watchlist = new Watchlist { TickerList = new List<string>() };
                        await _context.Watchlist.AddAsync(watchlist);
                        await _context.SaveChangesAsync();
                    }

                    var updatedList = await _context.Watchlist.FirstOrDefaultAsync();

                    updatedList.TickerList.Add(ticker.Symbol);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TickerExists(ticker.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(ticker);
        }

        // GET: Ticker/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ticker = await _context.Ticker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticker == null) return NotFound();

            return View(ticker);
        }

        // POST: Ticker/Delete/5
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ActionName("Delete")]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticker = await _context.Ticker.FindAsync(id);

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var trader = await _context.Traders.FirstOrDefaultAsync(x => x.IdentityUserId == userId);
            if (trader == null)
            {
                var newTrader = new Trader() { IdentityUserId = userId };
                await _context.Traders.AddAsync(newTrader);
                ticker.Traders.Add(newTrader);
                await _context.SaveChangesAsync();
            }
            else
            {
                ticker.Traders.Add(trader);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TickerExists(int id)
        {
            return _context.Ticker.Any(e => e.Id == id);
        }
    }
}