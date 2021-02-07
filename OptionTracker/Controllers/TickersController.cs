using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Data;
using OptionTracker.Models;

namespace OptionTracker.Controllers
{
    [Authorize]
    public class TickersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TickersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ticker
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ticker.ToListAsync());
        }

        // GET: Ticker/Details/5
        [HttpGet("Tickers/Details/{symbol}")]
        public async Task<IActionResult> Details(string symbol)
        {
            if (symbol == null)
            {
                return NotFound();
            }

            var ticker = await _context.Ticker
                .FirstOrDefaultAsync(m => m.Symbol.Equals(symbol.ToUpper()));

            if (ticker == null)
            {
                return NotFound();
            }

            var chainRaw = await _context.ChainRaw.Where(x => x.Chain.Symbol.Equals(ticker.Symbol))
                .OrderByDescending(x => x.Chain.Created)
                .FirstAsync();
            var optionContract = chainRaw.Chain.OptionContracts.OrderByDescending(x => x.OpenInterest)
                .Take(20).ToList();

            var viewModel = new ChainResultViewModel();

                viewModel = new ChainResultViewModel
                {
                    Ticker = chainRaw.Chain.Symbol,
                    Created = chainRaw.Chain.Created,

                    OptionsResults = optionContract
                     .Select(both => new OptionResultViewModel
                     {
                         Id = both.Symbol,
                         Description = both.Description,
                         OpenInterest = both.OpenInterest,
                         ClosePrice = both.ClosePrice,
                         OpenInterestChange = both.OpenInterest,
                         ClosePriceChange = both.ClosePrice
                     })
                     .ToList()
                };

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
                _context.Add(ticker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticker);
        }

        // GET: Ticker/Edit/5
        [HttpGet("Tickers/Edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sy = id.Split("_")[0];
            var chainRaw = await _context.ChainRaw.Where(x => x.Chain.Symbol.Equals(sy))
               .OrderByDescending(x => x.Chain.Created)
               .FirstAsync();

            var op = chainRaw.Chain.OptionContracts.Where(x=>x.Symbol.Equals(id))
                .OrderByDescending(x => x.QuoteTimeInLong)
                .First();

            if (op == null)
            {
                return NotFound();
            }
            return View(op);
        }

        // POST: Ticker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Symbol")] Ticker ticker)
        {
            if (id != ticker.Id)
            {
                return NotFound();
            }

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
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticker);
        }

        // GET: Ticker/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticker = await _context.Ticker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticker == null)
            {
                return NotFound();
            }

            return View(ticker);
        }

        // POST: Ticker/Delete/5
        [HttpPost, ActionName("Delete")]
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
