﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OptionTracker.Data;
using OptionTracker.Models;
using OptionTracker.Services;

namespace OptionTracker.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ticker.ToListAsync());
        }

        // GET: Ticker/Details/5
        [HttpGet("Tickers/TotalDetails/")]
        [Route("Tickers/TotalDetails/{id}")]
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
        // GET: Ticker/Details/5
        [HttpGet("Tickers/Details/{symbol}")]
        [Route("Tickers/Details/{symbol}/{id}")]
        public async Task<IActionResult> Details(string symbol, string? id)
        {
            if (symbol == null) return NotFound();
            var ticker = await _context.Ticker
                .FirstOrDefaultAsync(m => m.Symbol.Equals(symbol.ToUpper()));

            if (ticker == null) return NotFound();
            if (id != null && id.Equals("update"))
            {
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

            var chainRaw = await _context.ChainRaw.Where(x => x.Chain.Symbol.Equals(ticker.Symbol))
                .OrderByDescending(x => x.Chain.Created)
                .FirstAsync();

            var oldChainRaw = await _context.ChainRaw.Where(x =>
                    x.Chain.Symbol.Equals(ticker.Symbol) &&  (chainRaw.Chain.Created - x.Chain.Created) > new TimeSpan(0,23,0,0,0) && chainRaw.Id != x.Id)
                .OrderByDescending(x => x.Chain.Created)
                .FirstOrDefaultAsync();

            var viewModel = new ChainResultViewModel();

            if (oldChainRaw == null)
                viewModel = new ChainResultViewModel
                {
                    Ticker = chainRaw.Chain.Symbol,
                    Created = chainRaw.Chain.Created,

                    OptionsResults = chainRaw.Chain.OptionContracts
                        .Select(both => new OptionResultViewModel
                        {
                            Description = both.Description,
                            OpenInterest = both.OpenInterest,
                            ClosePrice = both.ClosePrice,
                            OpenInterestChange = both.OpenInterest - both.OpenInterest,
                            ClosePriceChange = both.ClosePrice - both.ClosePrice
                        })
                        .ToList()
                };
            else
                viewModel = new ChainResultViewModel
                {
                    Ticker = chainRaw.Chain.Symbol,
                    Created = chainRaw.Chain.Created,


                    TimeChange = chainRaw.Chain.Created - oldChainRaw.Chain.Created,

                    OptionsResults = chainRaw.Chain.OptionContracts.OrderByDescending(x=>x.Symbol).Zip(oldChainRaw.Chain.OptionContracts.OrderByDescending(x => x.Symbol), (a, b) => new {a, b})
                        .Select(both => new OptionResultViewModel
                        {
                            Description = both.a.Description,
                            OpenInterest = both.a.OpenInterest,
                            ClosePrice = both.a.ClosePrice,
                            OpenInterestChange = both.a.OpenInterest - both.b.OpenInterest,
                            ClosePriceChange = both.a.ClosePrice - both.b.ClosePrice
                        })
                        .ToList()
                };

            viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.OpenInterest).Take(20).ToList();

            if (id != null && id.Equals("true"))
                viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.TotalValue).ToList();

            if (id != null && id.Equals("oChange"))
                viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.OpenInterestChange).ToList();

            if (id != null && id.Equals("cChange"))
                viewModel.OptionsResults = viewModel.OptionsResults.OrderByDescending(x => x.ClosePriceChange).ToList();

            

            return View(viewModel);
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
        public async Task<IActionResult> Create([Bind("Id,Symbol")] Ticker ticker)
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
        [HttpGet("Tickers/Edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null) return NotFound();

            var sy = id.Split("_")[0];
            var chainRaw = await _context.ChainRaw.Where(x => x.Chain.Symbol.Equals(sy))
                .OrderByDescending(x => x.Chain.Created)
                .FirstAsync();

            var op = chainRaw.Chain.OptionContracts.Where(x => x.Symbol.Equals(id))
                .OrderByDescending(x => x.QuoteTimeInLong)
                .First();

            if (op == null) return NotFound();
            return View(op);
        }

        // POST: Ticker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Symbol")] Ticker ticker)
        {
            if (id != ticker.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var defaultWatchlist = await _context.Watchlist.FirstOrDefaultAsync();
                    if (defaultWatchlist == null)
                    {
                        var watchlist = new Watchlist {TickerList = new List<string>()};
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
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticker = await _context.Ticker.FindAsync(id);
            _context.Ticker.Remove(ticker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TickerExists(int id)
        {
            return _context.Ticker.Any(e => e.Id == id);
        }
    }
}